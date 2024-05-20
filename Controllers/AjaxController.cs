using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SchoolFinderWeb.Data;
using SchoolFinderWeb.Models;
using SchoolFinderWeb.ViewModels;
using System.Dynamic;

namespace SF.Controllers
{
    public class AjaxController : Controller
    {
        private readonly ApplicationDbContext schoolDB;
        private readonly UserManager<User> userManager;

        public AjaxController(ApplicationDbContext _schoolDB, UserManager<User> _userManager)
        {
            schoolDB = _schoolDB;
            userManager = _userManager;
        }

        [HttpPost]
        public async Task<IActionResult> AddFavorites([FromBody] FavoriteViewModel model)
        {

            var userId = userManager.GetUserId(User);
            //var userId = 1;


            if (userId == null)
            {
                return Unauthorized();
            }

            var favoriteItem = new FavoriteSchool
            {
                UserID = userId,
                SchoolID = model.SchoolID
            };

            schoolDB.FavoriteSchools.Add(favoriteItem);
            await schoolDB.SaveChangesAsync();

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> DelFromFavorites([FromBody] FavoriteViewModel model)
        {

            var userId = userManager.GetUserId(User);
            //var userId = 1;


            if (userId == null)
            {
                return Unauthorized();
            }

            var favoriteItem = schoolDB.FavoriteSchools.FirstOrDefault(f => f.UserID == userId && f.SchoolID == model.SchoolID);

            if(favoriteItem != null)
            {
                schoolDB.FavoriteSchools.Remove(favoriteItem);
                await schoolDB.SaveChangesAsync();

                return Ok();
            }

            return StatusCode(500);


        }

    }
}
