using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolFinderWeb.Data;
using SchoolFinderWeb.Models;

namespace SchoolFinderWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    //[Authorize]
    public class ArticlesController : Controller
    {
        private readonly ApplicationDbContext schoolDB;

        public ArticlesController(ApplicationDbContext schoolDB)
        {
            this.schoolDB = schoolDB;
        }

        public IActionResult Index()
        {
            var articles = schoolDB.Article;
            return View(articles);
        }

        //GET
        public IActionResult Create()
        {
            return View();
        }

        //POST
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Create(Article obj)
        {
            if (ModelState.IsValid)
            {
                schoolDB.Article.Add(obj);
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
            var articleId = schoolDB.Article.FirstOrDefault(c => c.ArticleID == id);

            if (articleId == null)
            {
                return NotFound();
            }

            return View(articleId);
        }

        //POST
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Edit(Article obj)
        {
            if (ModelState.IsValid)
            {
                schoolDB.Article.Update(obj);
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
            var article = schoolDB.Article.FirstOrDefault(c => c.ArticleID == id);

            return View(article);
        }


        //POST
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Delete(int id)
        {
            var obj = schoolDB.Article.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            schoolDB.Article.Remove(obj);
            schoolDB.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
