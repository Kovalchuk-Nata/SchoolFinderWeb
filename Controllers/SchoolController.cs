using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using SchoolFinderWeb.Data;
using SchoolFinderWeb.Models;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Xml.Linq;

namespace SchoolFinderWeb.Controllers
{
    public class SchoolController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext schoolDB;
        private readonly UserManager<User> _userManager;

        public SchoolController(ILogger<HomeController> logger, ApplicationDbContext schoolDB, UserManager<User> userManager)
        {
            _logger = logger;
            this.schoolDB = schoolDB;
            this._userManager = userManager;
        }
      

        //GET
        public IActionResult SchoolProfile()
        {
            var userId = _userManager.GetUserId(User);
            var school = schoolDB.School.FirstOrDefault(c => c.UserId == userId);

            Console.WriteLine("Id school: " + school);
            if (school == null)
            {
                Console.WriteLine("Id school 2: " + school);
                return NotFound();
            }
            
            return View(school);
        }


        [Route("/addSchool")]
        public async Task<IActionResult> AddSchool()
        {
            var userId = _userManager.GetUserId(User);
            var school = await schoolDB.School.FirstOrDefaultAsync(s => s.UserId == userId);

            if (school == null)
            {
                return View();
            }

            return RedirectToAction("SchoolProfile");
        }

        /*
        public async Task<IActionResult> AddSchool()
        {
            // проверка нет ли уже созданной юзером школы
            var userid = _usermanager.getuserid(user);
            var user = await schooldb.users.firstordefaultasync(u => u.id == userid); // додаємо await
            return View();
            

            //if (user == null)
            //{
            //    return NotFound(); // Оброблення ситуації, коли користувача не знайдено
            //}

            //return View(user); // Передача користувача до подання
        }
        */

        //POST
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult AddSchool(School obj)
        {
            if (ModelState.IsValid)
            {
                schoolDB.School.Add(obj);
                schoolDB.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        /*
        [Route("/editSchool")]
        public async Task<IActionResult> EditSchool()
        {
            //var schoolId = 

            return View();
        }

        //GET
        public IActionResult Edit(int? id)
        {
            Console.WriteLine("Id school: " + id);
            if (id == null || id == 0)
            {
                Console.WriteLine("Id school 2: " + id);
                return NotFound();
            }
            var schoolId = schoolDB.School.FirstOrDefault(c => c.SchoolID == id);

            if (schoolId == null)
            {
                return NotFound();
            }

            return View(schoolId);
        }

        //POST
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Edit(School obj)
        {
            if (ModelState.IsValid)
            {
                schoolDB.School.Update(obj); // Update(obj);
                schoolDB.SaveChanges(true);
                return RedirectToAction("Index");
            }
            return View(obj);
        }
        */
    }
}
