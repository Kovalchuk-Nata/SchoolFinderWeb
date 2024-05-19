using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolFinderWeb.Data;
using SchoolFinderWeb.Models;
using System.Diagnostics;

namespace SchoolFinderWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext schoolDB;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext schoolDB)
        {
            _logger = logger;
            this.schoolDB = schoolDB;
        }


        [Route("")]
        [Route("/home")]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Parent")]
        [Route("/contacts")]
        public IActionResult Contacts()
        {
            return View();
        }

        /*
        [Route("/favorites")]
        public IActionResult Favorites()
        {
            //var userId = userManager.GetUserId(User);
            var userId = 1;

            var favorites = schoolDB.FavoriteSchools.Include(f => f.School).Where(f => f.UserID == userId).ToList();

            return View(favorites);
        }
        */
        public IActionResult Privacy()
        {
            return View();
        }
          
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}