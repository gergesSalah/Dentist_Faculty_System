using DentalProject_Graduation.Data;
using DentalProject_Graduation.Data.Entities;
using DentalProject_Graduation.Data.Helper;
using DentalProject_Graduation.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IO;



namespace DentalProject_Graduation.Controllers
{
  
    public class ProfileController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly UserManager<ApplicationUser> UserManager;
        public ProfileController(ApplicationDbContext context,

        UserManager<ApplicationUser> userManager

           )
        {
            db = context;
            UserManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }

       
        public  async Task< IActionResult> Student()
        {

            var user = await UserManager.GetUserAsync(User);

            var Student =  db.DentalStudents.Where(x => x.User == user).SingleOrDefault();
            ViewBag.Student = Student;
            ViewBag.RecoveCases =  db.CaseInformation.Where(x => x.PhotoAfter != null && x.DentalStudentId == Student.DentalStudentId).Count();
            ViewBag.infectedCases = db.CaseInformation.Where(x => x.PhotoAfter == null && x.DentalStudentId == Student.DentalStudentId).Count();
            ViewBag.Total = ViewBag.RecoveCases + ViewBag.infectedCases;
            return View(user);
        }
        public async Task<IActionResult> Patient()
        {
            var user = await UserManager.GetUserAsync(User);
            var Patient = db.Patients.Where(x => x.user == user).SingleOrDefault();
            ViewBag.Patient = Patient;
            var casesPatient = db.Cases.Where(x => x.PatientId == Patient.PatientId).ToList();
            ViewBag.TreatedCases = db.Cases.Join(db.CaseInformation,
                cases=>cases.CaseId,
                caseinfo=>caseinfo.CaseId,
                (cases, caseinfo)=>new { cases = cases , caseinfo = caseinfo }
                ).Where(x=>x.caseinfo.PhotoAfter!=null && x.cases.PatientId== Patient.PatientId).Count();
            ViewBag.InfectedCases = db.Cases.Join(db.CaseInformation,
                cases => cases.CaseId,
                caseinfo => caseinfo.CaseId,
                (cases, caseinfo) => new { cases = cases, caseinfo = caseinfo }
                ).Where(x => x.caseinfo.PhotoAfter == null&&x.cases.PatientId == Patient.PatientId).Count();

            ViewBag.Total = db.Cases.Where(x => x.PatientId == Patient.PatientId).Count();
            ViewBag.noConnect = ViewBag.Total - (ViewBag.InfectedCases + ViewBag.TreatedCases);




            return View(user);
        }

        public string test()
        {
            var data = "Test Jason";
            return  data;
        }
        //make Partial View To Dispaly Cases Of Dental Student but this Case not Recover
         
        public  PartialViewResult InfectedCases()
        {
       
            string Id = UserManager.GetUserId(User);
            var user = db.Users.Find(Id);
            var student = db.DentalStudents.Where(x => x.User == user).SingleOrDefault();
            var AllinfectedCases = db.CaseInformation.Join(db.Cases,
                a => a.CaseId,
                b => b.CaseId,
                (a, b) => new { A = a, B = b }
                ).Join(db.Patients,
                ab => ab.B.PatientId,
                c => c.PatientId,
                (ab, c) => new { A = ab.A, B = ab.B, C = c }
                ).Join(db.Users,
                ac => ac.C.UserId,
                y => y.Id,
                (ac, y) => new { A = ac.A, B = ac.B, C = ac.C, Y = y }
                ).Where(filter => filter.A.PhotoAfter == null&&student.DentalStudentId==filter.A.DentalStudentId)
                .Select(x => new InfectedCasesVM { Firstname=x.Y.FirstName,Lastname=x.Y.LastName,phone=x.Y.PhoneNumber,Age=x.C.Age,
                    DiseasePicture = x.B.DiseasePicture,
                    DiseaseId = x.B.DiseaseId,
                    CaseInformationID = x.A.CaseInformationId,
                    Appoinment=x.A.Appointment,
                    DiseaseName = db.Diseases.Where(C => C.DiseaseId == x.B.DiseaseId).Select(o => o.DiseaseName).FirstOrDefault()
                }).ToList();
         
             
			return PartialView("~/Views/Shared/Student/_InfectedCases.cshtml", AllinfectedCases);
        }



        public PartialViewResult InfectedCasesPatient()
        {
            string Id = UserManager.GetUserId(User);
            var user = db.Users.Find(Id);
            var Patient = db.Patients.Where(x => x.UserId == user.Id).FirstOrDefault();
            var infectedCases = db.Cases.Join(db.CaseInformation,
                CaseP => CaseP.CaseId,
                CaseInfoP => CaseInfoP.CaseId,
                (CaseP, CaseInfoP) => new { CaseP = CaseP, CaseInfoP = CaseInfoP }
                ).Join(db.DentalStudents,
                TotalCases => TotalCases.CaseInfoP.DentalStudentId,
                dental => dental.DentalStudentId,
                (TotalCases, dental) => new { CaseP = TotalCases.CaseP, CaseInfoP = TotalCases.CaseInfoP, dental = dental }
                ).Join(db.Users,
                TotalTal => TotalTal.dental.User,
                user => user,
                (TotalTal, user) => new { CaseP = TotalTal.CaseP, CaseInfoP = TotalTal.CaseInfoP, dental = TotalTal.dental, user = user }
                ).Join(db.Diseases,
                allInfo => allInfo.CaseP.DiseaseId,
                disease => disease.DiseaseId,
                (allInfo, disease) => new {  CaseP = allInfo.CaseP, CaseInfoP = allInfo.CaseInfoP, dental = allInfo.dental, user = allInfo.user, disease = disease }
                ).Select(x => new InfectedCasePatientVM
                {
                    CaseId = x.CaseP.CaseId,
                    DiseaseName = x.disease.DiseaseName,
                    dentalNameF = x.user.FirstName,
                    dentalNameL = x.user.LastName,
                    Phone = x.user.PhoneNumber,
                    Appointment = x.CaseInfoP.Appointment,
                    DiseasePhoto = x.CaseP.DiseasePicture,
                    PhotoAfter = x.CaseInfoP.PhotoAfter,
                    PatientId=x.CaseP.PatientId

                }).Where(x=>x.PhotoAfter==null&&x.PatientId== Patient.PatientId&&x.Appointment!=null).ToList();



            return PartialView("~/Views/Shared/Patient/_InfectedCasesPatient.cshtml", infectedCases);

        }
        public PartialViewResult InfectedCasesNotContact()
        {
            string Id = UserManager.GetUserId(User);
            var patient = db.Patients.Where(x => x.UserId == Id).SingleOrDefault();
            List <CasesNotContactcs> CasesNotContactcs = new List<CasesNotContactcs>();
            var cases = db.Cases.Where(x => x.PatientId == patient.PatientId);
            foreach(var item in cases)
            {
                var ConfirmConnectCase = db.CaseInformation.Where(x => x.CaseId == item.CaseId).Count();
                if (ConfirmConnectCase <= 0)
                {
                    var caseNot = new CasesNotContactcs();
                    caseNot.CaseId = item.CaseId;
                    caseNot.DiseaseName = db.Diseases.Where(x => x.DiseaseId == item.DiseaseId).Select(x => x.DiseaseName).SingleOrDefault();
                    caseNot.photo = item.DiseasePicture;
                    CasesNotContactcs.Add(caseNot);

                }
            }

            return PartialView("~/Views/Shared/Patient/_infectedCasesnotContact.cshtml", CasesNotContactcs);

        }

        public PartialViewResult TreatedCasePatient()
        {
            string Id = UserManager.GetUserId(User);
            var user = db.Users.Find(Id);
            var Patient = db.Patients.Where(x => x.UserId == user.Id).FirstOrDefault();
            var TreatedCases = db.Cases.Join(db.CaseInformation,
                CaseP => CaseP.CaseId,
                CaseInfoP => CaseInfoP.CaseId,
                (CaseP, CaseInfoP) => new { CaseP = CaseP, CaseInfoP = CaseInfoP }
                ).Join(db.DentalStudents,
                TotalCases => TotalCases.CaseInfoP.DentalStudentId,
                dental => dental.DentalStudentId,
                (TotalCases, dental) => new { CaseP = TotalCases.CaseP, CaseInfoP = TotalCases.CaseInfoP, dental = dental }
                ).Join(db.Users,
                TotalTal => TotalTal.dental.User,
                user => user,
                (TotalTal, user) => new { CaseP = TotalTal.CaseP, CaseInfoP = TotalTal.CaseInfoP, dental = TotalTal.dental, user = user }
                ).Join(db.Diseases,
                allInfo => allInfo.CaseP.DiseaseId,
                disease => disease.DiseaseId,
                (allInfo, disease) => new { CaseP = allInfo.CaseP, CaseInfoP = allInfo.CaseInfoP, dental = allInfo.dental, user = allInfo.user, disease = disease }
                ).Select(x => new InfectedCasePatientVM
                {
                    CaseId = x.CaseP.CaseId,
                    DiseaseName = x.disease.DiseaseName,
                    dentalNameF = x.user.FirstName,
                    dentalNameL = x.user.LastName,
                    Phone = x.user.PhoneNumber,
                    Appointment = x.CaseInfoP.Appointment,
                    DiseasePhoto = x.CaseP.DiseasePicture,
                    PhotoAfter = x.CaseInfoP.PhotoAfter,
                    PatientId=x.CaseP.PatientId

                }).Where(x => x.PhotoAfter != null && x.PatientId == Patient.PatientId&&x.Appointment!=null).ToList();



            return PartialView("~/Views/Shared/Patient/_TreatedCasesPatient.cshtml", TreatedCases);



        }

        /// <summary>
        /// استني  لحد لما  ميخلص صفحة الهوم
        /// </summary>
        /// <returns></returns>
        public PartialViewResult AllCasesPatient()
        {
            var allCases = db.Cases.Join(db.Diseases,
                cases => cases.DiseaseId,
                dise => dise.DiseaseId,
                (cases, dise) => new { cases = cases, dise = dise }
                ).Select(x => new AllCasesPatient
                {
                    CaseId = x.cases.CaseId,
                    DiseaseId = x.dise.DiseaseId,
                    DiseaseName = x.dise.DiseaseName,
                    DiseasePicture = x.cases.DiseasePicture
                }).ToList();



        return PartialView("~/Views/Shared/Patient/_AllCasesPatient.cshtml", allCases);

        }
        public PartialViewResult TreatedCases()
        {


            string Id = UserManager.GetUserId(User);
            var user = db.Users.Find(Id);
            var student = db.DentalStudents.Where(x => x.User == user).SingleOrDefault();
            var AllTreatedCases = db.CaseInformation.Join(db.Cases,
                a => a.CaseId,
                b => b.CaseId,
                (a, b) => new { A = a, B = b }
                ).Join(db.Patients,
                ab => ab.B.PatientId,
                c => c.PatientId,
                (ab, c) => new { A = ab.A, B = ab.B, C = c }
                ).Join(db.Users,
                ac => ac.C.UserId,
                y => y.Id,
                (ac, y) => new { A = ac.A, B = ac.B, C = ac.C, Y = y }
                ).Where(filter => filter.A.PhotoAfter != null&&student.DentalStudentId==filter.A.DentalStudentId)
                .Select(x => new InfectedCasesVM
                {
                    Firstname = x.Y.FirstName,
                    Lastname = x.Y.LastName,
                    phone = x.Y.PhoneNumber,
                    Age = x.C.Age,
                    DiseasePicture = x.B.DiseasePicture,
                    DiseaseId = x.B.DiseaseId,
                    CaseInformationID = x.A.CaseInformationId,
                    photoAfter = x.A.PhotoAfter,
                    DiseaseName=db.Diseases.Where(C=>C.DiseaseId==x.B.DiseaseId).Select(o=>o.DiseaseName).FirstOrDefault()
              

                }).ToList();


            return PartialView("~/Views/Shared/Student/_TreatedCases.cshtml", AllTreatedCases);
        }

        [HttpGet]
        public IActionResult FinishCase(int id)
        {
            ViewBag.ID = id;
            return View();
        }

        [HttpPost]
        public IActionResult FinishCase(IFormFile ImageAfter,int id)
        {
            if(ImageAfter != null&&id!=null)
            {
                var caseinformtion = db.CaseInformation.Find(id);
                byte[] ImageData;
                using (var binaryReader = new BinaryReader(ImageAfter.OpenReadStream()))
                {
                    ImageData = binaryReader.ReadBytes((int)ImageAfter.Length);

                }
                caseinformtion.PhotoAfter = ImageData;
                db.SaveChanges();
                return RedirectToAction("Student", "profile");
            }
            else
            {

                ViewBag.erro = "Erro in Save Data Try Again !";
                ViewBag.ID = id;
                return View();
            }
           


        
        }

        public IActionResult GetDises()
        {
            ViewBag.data = db.Diseases.Join(db.Cases,
               a => a.DiseaseId,
               b => b.DiseaseId,
               (a, b) => new { A = a, b }
               ).Join(db.Patients,
               ab => ab.b.PatientId,
               c => c.PatientId,
               (ab, c) => new { A = ab.A, B = ab.b, C = c }
               ).Join(db.Users,
               abc => abc.C.UserId,
               v => v.Id,
               (abc, v) => new { A = abc.A, B = abc.B, C = abc.C, V = v }
               ).Select(x => new AllDataVm { fname = x.V.FirstName, age = x.C.Age, CaseId = x.B.CaseId, Dname = x.A.DiseaseName }).ToList();
            return View(ViewBag.data);

        }

       

        [HttpPost]
        public IActionResult DeleteCase(int Id)
        {
            if (Id == null)
            {
                return RedirectToAction("Patient");
            }
            else
            {
                try
                {
                    var CasePatient = db.Cases.Find(Id);
                    db.Cases.Remove(CasePatient);
                    db.SaveChanges();
                    return RedirectToAction("Patient");
                }
                catch
                {
                    return RedirectToAction("Patient");
                }
            }
        


        }



        public PartialViewResult TakeImage()
        {
            return PartialView("~/Views/Shared/Patient/TakeImage.cshtml");
        }


        [HttpPost]

        public async Task<IActionResult> Final()
        {
            var user =await UserManager.GetUserAsync(User);

            var patientId = db.Patients.Where(x => x.UserId == user.Id).Select(x => x.PatientId).SingleOrDefault();
            FormFile disImage = (FormFile)Request.Form.Files["fileup"];
            string diseName = Request.Form["pre"];
            string diseName2 = null;
           
            var idDisease = db.Diseases.Where(x => x.DiseaseName == diseName).Select(x => x.DiseaseId).SingleOrDefault();
          
            var NewCase = new Case();
            NewCase.DiseaseId = idDisease;
            NewCase.Viald = 0;
            NewCase.PatientId = patientId;
            ////
           var newCase2 = new Case();
            var idDisease2 = 0;
            if (diseName2 != null)
            {
                 idDisease2 = db.Diseases.Where(x => x.DiseaseName == diseName2).Select(x => x.DiseaseId).SingleOrDefault();
               
                newCase2.DiseaseId = idDisease2;
                newCase2.Viald = 0;
                newCase2.PatientId = patientId;
                byte[] ImageData1;
                using (var binaryReader = new BinaryReader(disImage.OpenReadStream()))
                {
                    ImageData1 = binaryReader.ReadBytes((int)disImage.Length);

                }
                NewCase.DiseasePicture = ImageData1;
                db.Cases.Add(newCase2);
                //db.SaveChanges();

            }

            byte[] ImageData;
            using (var binaryReader = new BinaryReader(disImage.OpenReadStream()))
            {
                ImageData = binaryReader.ReadBytes((int)disImage.Length);

            }
            NewCase.DiseasePicture= ImageData;

            db.Cases.Add(NewCase);

            db.SaveChanges();

            // alarm student
            List<Alarm> alarms = db.Alarms.ToList();
           
            for (int i = 0; i < alarms.Count(); i++)
            {
                if (alarms[i].IdDiseaase == idDisease)
                {
                    var d = db.DentalStudents.Where(x => x.DentalStudentId == alarms[i].IdDentail).SingleOrDefault();
                    var emailDetal = db.Users.Find(d.UserId);
                    SenderMail.SendMailA(diseName+ "was diagnosed and added to the site", "The condition of the teeth you are looking for has been added to the site. Diagnosis of dental diseases. You can now communicate with them. \"Open the site and go to the home page.\"\r\n", emailDetal.Email);
                    db.Alarms.Remove(alarms[i]);
                    db.SaveChanges();
                }
                if (alarms[i].IdDiseaase == idDisease2)
                {
                    var d = db.DentalStudents.Where(x => x.DentalStudentId == alarms[i].IdDentail).SingleOrDefault();
                    var emailDetal = db.Users.Find(d.User);
                    SenderMail.SendMailA(diseName + "was diagnosed and added to the site", "The condition of the teeth you are looking for has been added to the site. Diagnosis of dental diseases. You can now communicate with them. \"Open the site and go to the home page.\"\r\n", emailDetal.Email);
                    db.Alarms.Remove(alarms[i]);
                    db.SaveChanges();
                }
            }




            return Ok(diseName);
          
        }

    } 
}

