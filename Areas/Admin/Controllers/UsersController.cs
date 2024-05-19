using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolFinderWeb.Data;
using SchoolFinderWeb.Models;
using System.Data;

namespace SF.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]

    // [Authorize]
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext schoolDB;

        public UsersController(ApplicationDbContext schoolDB)
        {
            this.schoolDB = schoolDB;
        }

        public IActionResult Index()
        {
            var users = schoolDB.Users;
            return View(users);
        }

        //GET
        public IActionResult Create()
        {
            return View();
        }

        //POST
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Create(User obj)
        {
            if (ModelState.IsValid)
            {
                schoolDB.Users.Add(obj);
                schoolDB.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        /*
        //TODO сделать правильную работу в редактировании юзера в поле пароль
        //GET
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var userId = schoolDB.Users.FirstOrDefault(c => c.UserID == id);

            if (userId == null)
            {
                return NotFound();
            }

            return View(userId);
        }

        //POST
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Edit(User obj)
        {
            if (ModelState.IsValid)
            {
                schoolDB.Users.Update(obj);
                schoolDB.SaveChanges();
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
            var user = schoolDB.Users.FirstOrDefault(c => c.UserID == id);

            return View(user);
        }


        //POST
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Delete(int id)
        {
            var obj = schoolDB.Users.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            schoolDB.Users.Remove(obj);
            schoolDB.SaveChanges();
            return RedirectToAction("Index");
        }
        */
    }
}
