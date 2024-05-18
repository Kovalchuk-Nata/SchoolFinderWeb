using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SchoolFinderWeb.Models;

namespace SchoolFinderWeb.Controllers
{
    [Authorize(Roles = "Parent")]
    public class ProfileController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        private readonly UserManager<User> _userManager;

        public ProfileController(RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        [Route("/profile")]
        public IActionResult Profile()
        {
            return View();
        }


    }
}
