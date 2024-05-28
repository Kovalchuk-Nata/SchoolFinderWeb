using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolFinderWeb.Data;
using SchoolFinderWeb.Models;
using SchoolFinderWeb.ViewModels;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using System.Web.Helpers;

namespace SchoolFinderWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext schoolDB;
        private readonly UserManager<User> userManager;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext schoolDB, UserManager<User> userManager)
        {
            _logger = logger;
            this.schoolDB = schoolDB;
            this.userManager = userManager;
        }


        [Route("")]
        [Route("/home")]
        public IActionResult Index()
        {
            return View();
        }


        [Route("/findschool")]
        public IActionResult FindSchool(FindSchoolViewModel model)
        {

            var districts = schoolDB.School.Select(s => s.District).Distinct().ToList();
            var types = schoolDB.School.Select(s => s.Type).Distinct().ToList();

            var filteredSchools = schoolDB.School.AsQueryable();

            if (model.SelectedDistricts != null && model.SelectedDistricts.Any())
            {
                filteredSchools = filteredSchools.Where(p => model.SelectedDistricts.Contains(p.District));
            }

            if (model.SelectedTypes != null && model.SelectedTypes.Any())
            {
                filteredSchools = filteredSchools.Where(p => model.SelectedTypes.Contains(p.Type));
            }

            if (model.MaxPrice.HasValue)
            {
                filteredSchools = filteredSchools.Where(p => p.Price <= model.MaxPrice.Value);
            }

            if (model.IsExtendedDayGroups.HasValue)
            {
                filteredSchools = filteredSchools.Where(p => p.ExtendedDayGroups == model.IsExtendedDayGroups.Value);
            }

            if (model.IsSpecialization.HasValue)
            {
                filteredSchools = filteredSchools.Where(p => p.Specialization == model.IsSpecialization.Value);
            }

            if (model.IsAdditionalOpportunities.HasValue)
            {
                filteredSchools = filteredSchools.Where(p => p.AdditionalOpportunities == model.IsAdditionalOpportunities.Value);
            }

            var viewModel = new FindSchoolViewModel
            {
                Districts = districts,
                Types = types,
                SelectedDistricts = model.SelectedDistricts ?? new List<string>(),
                SelectedTypes = model.SelectedTypes ?? new List<string>(),
                MaxPrice = model.MaxPrice,
                IsExtendedDayGroups = model.IsExtendedDayGroups,
                IsSpecialization = model.IsSpecialization,
                IsAdditionalOpportunities = model.IsAdditionalOpportunities,
                Schools = filteredSchools.ToList()
            };

            return View(viewModel);
        }


        [Route("/articles")]
        public IActionResult Articles()
        {
            var articles = schoolDB.Article;
            return View(articles);
        }


        [Route("/articles/{id}")]
        public IActionResult ArticleView(int id)
        {
            var article = schoolDB.Article.FirstOrDefault(a => a.ArticleID == id);
            if (article == null)
            {
                return NotFound();
            }
            return View(article);
        }


        [Route("/findschool/{id}")]
        public IActionResult FindSchoolView(int id)
        {
            var _school = schoolDB.School.FirstOrDefault(a => a.SchoolID == id);
            if (_school == null)
            {
                return NotFound();
            }

            var likesCount = GetLikesCount(id);
            var dislikesCount = GetDislikesCount(id);

            // Збираємо дані про кількість олімпіадників за роками
            var olympiadData = schoolDB.VUO
                .Where(v => v.SchoolId == id)
                .GroupBy(v => v.OlimpiadYear)
                .Select(g => new OlympiadDataByYear
                {
                    Year = g.Key,
                    Count = g.Count()
                })
                .ToList();

            var olympiadDataByMath = schoolDB.VUO
                 .Where(v => v.SchoolId == id && v.Subject == "Математика")
                 .GroupBy(v => v.OlimpiadYear)
                 .Select(g => new OlympiadDataByYear
                {
                    Year = g.Key,
                    Count = g.Count()
                })
                .ToList();

            var olympiadDataByUkr = schoolDB.VUO
                 .Where(v => v.SchoolId == id && v.Subject == "Українська мова")
                 .GroupBy(v => v.OlimpiadYear)
                 .Select(g => new OlympiadDataByYear
                 {
                     Year = g.Key,
                     Count = g.Count()
                 })
                .ToList();

            var viewModel = new SchoolProfileViewModel
            {
                School = _school,
                LikesCount = likesCount,
                DislikesCount = dislikesCount,
                OlympiadDataByYear = olympiadData,
                OlympiadDataByMath = olympiadDataByMath,
                OlympiadDataByUkr = olympiadDataByUkr
            };

            return View(viewModel);
        }

        public int GetLikesCount(int schoolId)
        {
            return schoolDB.Likes.Count(l => l.SchoolID == schoolId && l.IsLike);
        }

        public int GetDislikesCount(int schoolId)
        {
            return schoolDB.Likes.Count(l => l.SchoolID == schoolId && !l.IsLike);
        }

        [Route("/contacts")]
        public IActionResult Contacts()
        {
            return View();
        }


        [Route("/favorites")]
        public IActionResult Favorites()
        {
            var userId = userManager.GetUserId(User);
            //var userId = 1;

            var favorites = schoolDB.FavoriteSchools.Include(f => f.School).Where(f => f.UserID == userId).ToList();

            return View(favorites);
        }

        [Route("/compare")]
        public IActionResult Compare()
        {
            var userId = userManager.GetUserId(User);

            var favorites = schoolDB.Compare.Include(f => f.School).Where(f => f.UserID == userId).ToList();

            return View(favorites);
        }

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