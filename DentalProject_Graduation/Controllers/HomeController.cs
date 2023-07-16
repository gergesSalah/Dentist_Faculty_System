using DentalProject_Graduation.Data;
using DentalProject_Graduation.Data.Entities;
using DentalProject_Graduation.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Buffers.Text;
using System.Diagnostics;
using DentalProject_Graduation.Data.Helper;
using NHibernate.Engine;
using static NHibernate.Engine.TransactionHelper;

namespace DentalProject_Graduation.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, UserManager<ApplicationUser> userManger)
        {
            _logger = logger;
            _userManager = userManger;
            _context = context;
        }

        //[Authorize(Roles = "Doctor")]
        public IActionResult Index()
        {
            var UserDoctor = _userManager.GetUserAsync(User).Result;
            var DectorId = _context.DentalStudents.Where(x => x.User == UserDoctor).Select(x => x.DentalStudentId).SingleOrDefault();

            AllDataVm dataVm = new AllDataVm();
            var result = from disease in _context.Diseases
                         join caseItem in _context.Cases on disease.DiseaseId equals caseItem.DiseaseId
                         join patient in _context.Patients on caseItem.PatientId equals patient.PatientId
                         join user in _context.Users on patient.UserId equals user.Id
                         where !_context.CaseInformation.Any(ci => ci.CaseId == caseItem.CaseId)
                         select new AllDataVm
                         {
                             fname = user.FirstName,
                             age = patient.Age,
                             DoseaseNAme=disease.DiseaseName,
                             CaseId = caseItem.CaseId,
                             lname = user.LastName,
                             Dname = disease.DiseaseName,
                             DiseaseDescription = disease.DiseaseDescription,
                             DiseasePicture = caseItem.DiseasePicture,
                             MailPatient=user.Email ,
                             



                         };
            var s = result.ToList();
            var groupeddate = s.GroupBy(d => d.Dname);
            return View(groupeddate.ToList());
        }

        public ActionResult Search(string term)
        {
            AllDataVm dataVm = new AllDataVm();
            var result = (from disease in _context.Diseases
                          join caseItem in _context.Cases on disease.DiseaseId equals caseItem.DiseaseId
                          join patient in _context.Patients on caseItem.PatientId equals patient.PatientId
                          join user in _context.Users on patient.UserId equals user.Id
                          where !_context.CaseInformation.Any(ci => ci.CaseId == caseItem.CaseId)
                          select new AllDataVm
                          {
                              fname = user.FirstName,
                              age = patient.Age,
                              DoseaseNAme = disease.DiseaseName,
                              CaseId = caseItem.CaseId,
                              lname = user.LastName,
                              Dname = disease.DiseaseName,
                              DiseaseDescription = disease.DiseaseDescription,
                              DiseasePicture = caseItem.DiseasePicture
                          }).ToList();
            if (term != null)
            {

                var lower= char.ToUpper(term[0]) + term.Substring(1).ToLower();
                var s = result.Where(a => a.Dname.Contains(term)
                || a.age.Contains(term)
                || a.fname.Contains(term)
                || a.lname.Contains(term)
                || (a.fname + a.lname).Contains(term)
                || a.Dname.Contains(lower)).ToList();
                var returnedsearchedgroupped = s.GroupBy(d => d.Dname);
                return View("Index", returnedsearchedgroupped.ToList());
            }
            else
            {
                var returnedsearchedgroupped = result.GroupBy(d => d.Dname);
                return View(nameof(Index), returnedsearchedgroupped.ToList());
            }

        }


        //Upload Photo
        //[Authorize (Roles ="Patient")]
        [HttpPost]
        public IActionResult UploadPhoto(IFormFile imagefile)
        {
            string userid = _userManager.GetUserId(User);

            var patid = _context.Patients
                .Join(_context.Users, patient => patient.UserId, user => user.Id, (patient, user) => new { patient, user })
                .Where(x => x.user.Id == userid)
                .Select(x => x.patient.PatientId).SingleOrDefault();
            if (imagefile != null && imagefile.Length > 0)
            {
                // Convert the image to a byte array
                byte[] imageBytes;
                using (var stream = new MemoryStream())
                {
                    imagefile.CopyTo(stream);
                    imageBytes = stream.ToArray();
                }
                Random random = new Random();


                // Create a new Case object and set the Image property
                var newCase = new Case();
                newCase.DiseasePicture = imageBytes;
                newCase.PatientId = patid;
                newCase.DiseaseId = random.Next(1, _context.Diseases.Count());
                _context.Cases.Add(newCase);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("imagefile", "Please select an image.");

                return View();
            }

        }

        // Task Of Connect DoctorWith Patient

        [HttpGet]
        public async Task<IActionResult> ConnectPatient(int id)
        {
            
            var Case= _context.Cases.Find(id);
            var patient = _context.Patients.Find(Case.PatientId);
            //var case_info=_context.CaseInformation.Where(x=>x.CaseId == id).FirstOrDefault();
            var userPatient = _context.Users.Find(patient.UserId);
            var diseas = _context.Diseases.Find(Case.DiseaseId);
            ApplicationUser currentuser =await _userManager.GetUserAsync(User);
            var dental = _context.DentalStudents.Where(x=>x.UserId== currentuser.Id).SingleOrDefault();



            ConnectedVM ConnectModel = new ConnectedVM();
            ConnectModel.fname = userPatient.FirstName;
            ConnectModel.lname=userPatient.LastName;
            ConnectModel.DentailId = dental.DentalStudentId;
            ConnectModel.Disease = diseas.DiseaseName;
            ConnectModel.CaseId= id;
            ConnectModel.MailPatient = userPatient.Email;
          




            return View(ConnectModel);
        }
        [HttpPost]
        [ActionName("ConnectPatient")]
        public IActionResult ConnectPatientFiinsh(ConnectedVM model)
        {
            
            if (ModelState.IsValid)
            {
                var user = _userManager.GetUserAsync(User).Result;
                CaseInformation caseInfo = new CaseInformation();
                caseInfo.CaseId = model.CaseId;
                caseInfo.DentalStudentId = model.DentailId;
                caseInfo.Appointment = model.Appointment;
                _context.CaseInformation.Add(caseInfo);
                _context.SaveChanges();
                var Status = SenderMail.SendMailA(
                    "Doctor " + user.FirstName + user.LastName + " is the one who treats you and follows you up ",
                    model.Message + Environment.NewLine + "------------------Appoinment : ------------------------ \n" + "Day: \t" + $"{model.Appointment.Day}/{model.Appointment.Month}/{model.Appointment.Year}\n" +
                    $"Time:\t {model.Appointment.Hour}--{model.Appointment.Minute}",
                    model.MailPatient
                    );
                if (Status == "good")
                {

                    return RedirectToAction("Index");

                }
                else
                {
                    ViewBag.err = "Send Mail Is Faild";
                    return View(model);
                }



            }

            return View(model);


        }

        public IActionResult Privacy()

        {
            List<Disease> Diseases = _context.Diseases.ToList();
            return View(Diseases);

        }
        public IActionResult About()
        {
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }
        public IActionResult Doctors()
        {
            return View();
        }
        public IActionResult Services()
        {
            return View();
        }
        public IActionResult Blog()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // notify dental Studeny about Disease
        [HttpPost]
        public IActionResult AlarmStudent(int DiseaseId)
        {

            // fisrt step
            var user = _userManager.GetUserAsync(User).Result;
            var AlarmStudent = new Alarm();
            AlarmStudent.IdDiseaase = DiseaseId;
            AlarmStudent.ApplyOn = DateTime.Now;
            AlarmStudent.IdDentail = _context.DentalStudents.Where(x => x.User == user).Select(c => c.DentalStudentId).FirstOrDefault();
            

            //
            var check=_context.Alarms.Any(x=>x.IdDiseaase == AlarmStudent.IdDiseaase&&x.IdDentail== AlarmStudent.IdDentail);
           
            if (check)
            {
                string satus = "You have already alerted the site may by case founded in site";


                return BadRequest(satus);

            }
     
            else
            {
                _context.Alarms.Add(AlarmStudent);
                _context.SaveChanges();
                string status = "The site has been successfully alerted";
                return Ok(status);
            }

        }
    }
}