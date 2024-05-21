using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolFinderWeb.Areas.Admin.ViewModel;
using SchoolFinderWeb.Areas.Identity.Pages.Account;
using SchoolFinderWeb.Data;
using SchoolFinderWeb.Models;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace SchoolFinderWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]

    // [Authorize]
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext schoolDB;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly IUserStore<User> _userStore;
        private readonly IUserEmailStore<User> _userEmailStore;

        public UsersController(ApplicationDbContext schoolDB,
            UserManager<User> userManager,
            IUserStore<User> userStore,
            SignInManager<User> signInManager,
            ILogger<RegisterModel> logger,
            IUserEmailStore<User> emailStore,
            IEmailSender emailSender)
        {
            this.schoolDB = schoolDB;
            _userManager = userManager;
            _userStore = userStore;
            _userEmailStore = emailStore;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }



        public IActionResult Index()
        {
            var users = schoolDB.Users;
            return View(users);
        }

        //GET
        public IActionResult Create()
        {

            return View(new InputModel());
        }

        //POST
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(InputModel obj)
        {
            //returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = CreateUser();

                user.FirstName = Input.FirstName;
                user.LastName = Input.LastName;
                user.UserType = Input.UserType;
                await _userStore.SetUserNameAsync(user, Input.Email, CancellationToken.None);
                await _userEmailStore.SetEmailAsync(user, Input.Email, CancellationToken.None);
                var result = await _userManager.CreateAsync(user, Input.Password);

                if (result.Succeeded)
                {
                    // Додати користувача до ролі
                    if (user.UserType == UserTypes.Parent)
                    {
                        _userManager.AddToRoleAsync(user, "Parent").Wait();
                    }
                    else if (user.UserType == UserTypes.Organizator)
                    {
                        _userManager.AddToRoleAsync(user, "Organizator").Wait();
                    }
                    else if (user.UserType == UserTypes.Admin)
                    {
                        _userManager.AddToRoleAsync(user, "Admin").Wait();
                    }


                    _logger.LogInformation("User created a new account with password.");

                    var userId = await _userManager.GetUserIdAsync(user);
                    user.EmailConfirmed = true;
                    await _userManager.UpdateAsync(user);

                    //await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index"); //LocalRedirect(returnUrl);
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            
            return View(obj);
        }

        ////POST
        //[HttpPost]
        //[AutoValidateAntiforgeryToken]
        //public IActionResult Create(User obj)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        schoolDB.Users.Add(obj);
        //        schoolDB.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(obj);
        //}

        
        //TODO сделать правильную работу в редактировании юзера в поле пароль
        //GET
        public IActionResult Edit(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userId = schoolDB.Users.FirstOrDefault(c => c.Id == id);

            if (userId == null)
            {
                return NotFound();
            }

            var userView = new EditUserViewModel
            {
                Id = userId.Id,
                UserType = userId.UserType,
                FirstName = userId.FirstName,
                LastName = userId.LastName
            };
            
            

            return View(userView);
        }

        //POST
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Edit(EditUserViewModel obj)
        {

            //if (ModelState.IsValid)
            //{
                var user = schoolDB.Users.FirstOrDefault(c => c.Id == obj.Id);

                if(user == null)
                {
                    return NotFound();
                }

                user.FirstName = obj.FirstName;
                user.LastName = obj.LastName;
                user.UserType = obj.UserType;
                schoolDB.Users.Update(user);
                schoolDB.SaveChanges();
            
            return RedirectToAction("Index");
            //}
            //return View(obj);

        }

        //GET
        public IActionResult Delete(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var user = schoolDB.Users.FirstOrDefault(c => c.Id == id);

            return View(user);
        }


        //POST
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Delete(User user)
        {
            var userId = user.Id;
            var obj = schoolDB.Users.Find(userId);
            if (obj == null)
            {
                return NotFound();
            }

            schoolDB.Users.Remove(obj);
            schoolDB.SaveChanges();
            return RedirectToAction("Index");
        }
        
        private User CreateUser()
        {
            try
            {
                return Activator.CreateInstance<User>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(User)}'. " +
                    $"Ensure that '{nameof(User)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }
    }
    public class InputModel
    {
        [Required]
        [Display(Name = "FirstName")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "LastName")]
        public string LastName { get; set; }

        [Display(Name = "UserType")]
        public UserTypes UserType { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }


        //[DataType(DataType.Password)]
        //[Display(Name = "Confirm password")]
        //[Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        //public string ConfirmPassword { get; set; }
    }
}
