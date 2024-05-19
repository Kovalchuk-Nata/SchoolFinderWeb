using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SchoolFinderWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    //[Authorize]
    public class HomesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
