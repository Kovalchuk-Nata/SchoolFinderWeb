using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolFinderWeb.Data;
using SchoolFinderWeb.Models;

namespace SchoolFinderWeb.Areas.Admin.Controllers
{
   [Area("Admin")]
   [Authorize(Roles = "Admin")]

    // [Authorize]
    public class SchoolsController : Controller
    {
        private readonly ApplicationDbContext schoolDB;

        public SchoolsController(ApplicationDbContext schoolDB)
        {
            this.schoolDB = schoolDB;
        }

        public IActionResult Index()
        {
            var schools = schoolDB.School;
            return View(schools);
        }

        //GET
        public IActionResult Create()
        {
            return View();
        }

        //POST
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Create(School obj)
        {
            if (ModelState.IsValid)
            {
                schoolDB.School.Add(obj);
                schoolDB.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
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

        //GET
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var school = schoolDB.School.FirstOrDefault(c => c.SchoolID == id);

            return View(school);
        }


        //POST
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Delete(int id)
        {
            var obj = schoolDB.School.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            schoolDB.School.Remove(obj);
            schoolDB.SaveChanges();
            return RedirectToAction("Index");
        }
        
    }
}
