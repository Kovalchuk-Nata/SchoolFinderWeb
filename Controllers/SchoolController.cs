using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using SchoolFinderWeb.Data;
using SchoolFinderWeb.Models;
using SchoolFinderWeb.ViewModels;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Xml.Linq;

namespace SchoolFinderWeb.Controllers
{
    [Authorize(Roles = "Organizator, Admin")]
    public class SchoolController : Controller
    {
        private readonly ApplicationDbContext schoolDB;
        private readonly UserManager<User> _userManager;

        public SchoolController(ILogger<HomeController> logger, ApplicationDbContext schoolDB, UserManager<User> userManager)
        {
            this.schoolDB = schoolDB;
            this._userManager = userManager;
        }


        //GET
        [Route("schoolProfile")]
        public IActionResult SchoolProfile()
        {
            var userId = _userManager.GetUserId(User);
            var school = schoolDB.School.FirstOrDefault(c => c.UserId == userId);

            Console.WriteLine("Id school: " + school);
            if (school == null)
            {
                Console.WriteLine("Id school 2: " + school);
                return View("Message", "Спочатку додайте школу.");
                //return NotFound();
            }

            return View(school);
        }


        [Route("/addSchool")]
        public async Task<IActionResult> Create()
        {
            var userId = _userManager.GetUserId(User);
            var school = await schoolDB.School.FirstOrDefaultAsync(s => s.UserId == userId);

            if (school == null)
            {
                SchoolViewModel schoolVM = new SchoolViewModel();
                return View(schoolVM);
                //return View();
            }

            return RedirectToAction("SchoolProfile");
        }

        //POST
        [Route("/addSchool")]
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Create(SchoolViewModel obj)
        {
            var userId = _userManager.GetUserId(User);

            School school = new School();
            school.Name = obj.Name;
            school.Address = obj.Address;
            school.District = obj.District;
            school.Type = obj.Type;
            school.Price = obj.Price;
            school.ExtendedDayGroups = obj.ExtendedDayGroups;
            school.Specialization = obj.Specialization;
            school.AdditionalOpportunities = obj.AdditionalOpportunities;
            school.Description = obj.Description;
            school.UserId = userId;

            schoolDB.School.Add(school);
            schoolDB.SaveChanges();
            return RedirectToAction("SchoolProfile");
        }

        //GET
        [Route("/editSchool")]
        public async Task<IActionResult> EditSchool()
        {
            var userId = _userManager.GetUserId(User);
            var school = await schoolDB.School.FirstOrDefaultAsync(s => s.UserId == userId);
            if (school == null)
            {
                return View("Message", "Спочатку додайте школу.");
            }
            return View(school);
        }


        //POST
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Edit(School obj)
        {
            if (ModelState.IsValid)
            {
                obj.isConfirmed = false;
                schoolDB.School.Update(obj);
                schoolDB.SaveChanges(true);
                return RedirectToAction("SchoolProfile");
            }
            return View(obj);
        }

        




    }
}
