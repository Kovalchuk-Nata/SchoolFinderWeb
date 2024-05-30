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

            var favorites = schoolDB.FavoriteSchools.Where(f => f.UserID == userId).Select(f => f.SchoolID).ToList() ?? new List<int>();

            var favoriteItem = new FavoriteSchool
            {
                UserID = userId,
                SchoolID = model.SchoolID
            };

            if (!favorites.Contains(model.SchoolID))
            {
                schoolDB.FavoriteSchools.Add(favoriteItem);
                await schoolDB.SaveChangesAsync();
            }


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

            if (favoriteItem != null)
            {
                schoolDB.FavoriteSchools.Remove(favoriteItem);
                await schoolDB.SaveChangesAsync();

                return Ok();
            }

            return StatusCode(500);


        }

        // COMPARISON
        [HttpPost]
        public async Task<IActionResult> AddCompare([FromBody] FavoriteViewModel model)
        {

            var userId = userManager.GetUserId(User);

            if (userId == null)
            {
                return Unauthorized();
            }

            var compareItem = new Compare
            {
                UserID = userId,
                SchoolID = model.SchoolID
            };

            var compares = schoolDB.Compare.Where(f => f.UserID == userId).Select(f => f.SchoolID).ToList() ?? new List<int>();

            if (!compares.Contains(model.SchoolID))
            {
                schoolDB.Compare.Add(compareItem);
                await schoolDB.SaveChangesAsync();
            }

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> DelFromCompare([FromBody] FavoriteViewModel model)
        {

            var userId = userManager.GetUserId(User);

            if (userId == null)
            {
                return Unauthorized();
            }

            var compareItem = schoolDB.Compare.FirstOrDefault(f => f.UserID == userId && f.SchoolID == model.SchoolID);

            if (compareItem != null)
            {
                schoolDB.Compare.Remove(compareItem);
                await schoolDB.SaveChangesAsync();

                return Ok();
            }

            return StatusCode(500);
        }

        // ADD LIKES AND DISLIKES
        [HttpPost]
        public async Task<IActionResult> AddReaction([FromBody] Likes model)
        {
            var userId = userManager.GetUserId(User);

            if (userId == null)
            {
                return Unauthorized();
            }

            var like = schoolDB.Likes.FirstOrDefault(l => l.UserID == userId && l.SchoolID == model.SchoolID);
            if (like == null)
            {
                like = new Likes
                {
                    UserID = userId,
                    SchoolID = model.SchoolID,
                    IsLike = model.IsLike
                };
                schoolDB.Likes.Add(like);
            }
            else
            {
                like.IsLike = model.IsLike;
                schoolDB.Likes.Update(like);
            }

            await schoolDB.SaveChangesAsync();

            var likesCount = schoolDB.Likes.Count(l => l.SchoolID == model.SchoolID && l.IsLike);
            var dislikesCount = schoolDB.Likes.Count(l => l.SchoolID == model.SchoolID && !l.IsLike);

            return Ok(new { LikesCount = likesCount, DislikesCount = dislikesCount });
        }
    }
}
