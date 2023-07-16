using DentalProject_Graduation.Data;
using DentalProject_Graduation.Data.Entities;
using DentalProject_Graduation.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Cryptography.X509Certificates;

namespace DentalProject_Graduation.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> UserManager;
        public AdminController(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            UserManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await UserManager.GetUserAsync(User);

            //count of user
            ViewBag.AllUser = _db.Users.Count();
            ViewBag.PatientUser = _db.UserRoles.Where(x => x.RoleId == "Do_1911").Count();
            ViewBag.DoctorUser = _db.UserRoles.Where(x => x.RoleId == "Pa_1911").Count();

            //count of cases
            ViewBag.AllCases = _db.Cases.Count();
            ViewBag.TreatedCase = _db.CaseInformation.Where(x => x.PhotoAfter != null).Count();


            //cases who not omuncate with him
            var nocoumncatecase = _db.Cases.Join(_db.CaseInformation,
                cases => cases.CaseId,
                caseinfo => caseinfo.CaseId,
                (cases, caseinfo) => new { cases = cases, caseinfo = caseinfo }
                ).Count();
            ViewBag.NotContactCases = ViewBag.AllCases - nocoumncatecase;

            return View(user);
        }

        public PartialViewResult DiseaseCases(int id)
        {
            var DiseaseCases = _db.Cases.Where(x => x.DiseaseId == id);

            return PartialView();
        }

        [HttpGet]
        public PartialViewResult EditCases()
        {
            var ViladCase = _db.Cases.Join(_db.Diseases,
                cases => cases.DiseaseId,
                dis => dis.DiseaseId,
                (cases, dis) => new
                {
                    Case = cases,
                    Disease = dis
                }


                ).Select(res => new ViladCases()
                {
                    IdCases = res.Case.CaseId,
                    IdDiseas = res.Disease.DiseaseId,
                    NameDiseas = res.Disease.DiseaseName,
                    photo = res.Case.DiseasePicture
                });
            return PartialView("~/Views/Shared/AdminPartial/AllCasesAdmin.cshtml", ViladCase);
        }


        [HttpPost]
        public IActionResult DeleteCase(int id)
        {
            var DeleteCase = _db.Cases.Find(id);
            _db.Cases.Remove(DeleteCase);
            _db.SaveChanges();

            return Ok();
        }


        [HttpPost]
        public IActionResult CorrectCase(int id)
        {
            var CorrectCase = _db.Cases.Find(id);
            CorrectCase.Viald = 1;
            _db.SaveChanges();

            return Ok();
        }

        [HttpGet]

        public PartialViewResult EditSelectType(int id)
        {


            ViewBag.TypeDisess = new SelectList(_db.Diseases, "DiseaseId", "DiseaseName");
            ViewBag.ID = id;
            var cases = _db.Cases.Find(id);
            ViewBag.photo = cases.DiseasePicture;

            return PartialView("~/Views/Shared/AdminPartial/ListDiseas.cshtml");

        }
        [HttpPost]
        public IActionResult FinalEdit(int id , int Diagnosis)
        {
            

            var cases=_db.Cases.Find(id);
            cases.DiseaseId = Convert.ToInt32(Diagnosis);
            _db.SaveChanges();

            return RedirectToAction("index");
        }



    }
}
