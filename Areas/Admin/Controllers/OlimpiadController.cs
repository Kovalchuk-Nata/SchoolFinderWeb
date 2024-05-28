using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolFinderWeb.Data;
using SchoolFinderWeb.Models;
using System.Data;

namespace SchoolFinderWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class OlimpiadController : Controller
    {
        private readonly ApplicationDbContext schoolDB;

        public OlimpiadController(ApplicationDbContext schoolDB)
        {
            this.schoolDB = schoolDB;
        }

        public IActionResult Index()
        {
            var olim = schoolDB.VUO;
            return View(olim);
        }

        //GET
        public IActionResult Create()
        {
            return View();
        }

        //POST
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Create(VUO obj)
        {
            if (ModelState.IsValid)
            {
                if (obj.SchoolId != null)
                {
                    var school = schoolDB.School.FirstOrDefault(s => s.SchoolID == obj.SchoolId);
                    if (school == null)
                    {
                        ModelState.AddModelError("SchoolId", "Даної школи немає в базі даних.");
                        return View(obj);
                    }
                }
                schoolDB.VUO.Add(obj);
                schoolDB.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //TODO нужно чтоб возвращало фото в статьях при едите
        //GET
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var olim = schoolDB.VUO.FirstOrDefault(c => c.Id == id);

            if (olim == null)
            {
                return NotFound();
            }

            return View(olim);
        }

        //POST
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Edit(VUO obj)
        {
            if (ModelState.IsValid)
            {
                schoolDB.VUO.Update(obj);
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
            var olim = schoolDB.VUO.FirstOrDefault(c => c.Id == id);

            return View(olim);
        }


        //POST
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Delete(int id)
        {
            var obj = schoolDB.VUO.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            schoolDB.VUO.Remove(obj);
            schoolDB.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
