using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolFinderWeb.Data;
using SchoolFinderWeb.Models;

namespace SchoolFinderWeb.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext schoolDB;
        private readonly UserManager<User> _userManager;

        public ProfileController(ILogger<HomeController> logger, ApplicationDbContext schoolDB, UserManager<User> userManager)
        {
            _logger = logger;
            this.schoolDB = schoolDB;
            this._userManager = userManager;
        }

        [Route("/profile")]
        public async Task<IActionResult> Profile()
        {
            var userId = _userManager.GetUserId(User);
            var user = await schoolDB.Users.FirstOrDefaultAsync(u => u.Id == userId); // Додаємо await

            if (user == null)
            {
                return NotFound(); // Оброблення ситуації, коли користувача не знайдено
            }

            return View(user); // Передача користувача до подання
        }


    }
}
