using SchoolFinderWeb.Models;
using System.ComponentModel.DataAnnotations;

namespace SchoolFinderWeb.Areas.Admin.ViewModel
{
    public class EditUserViewModel
    {
        public string Id { get; set; }

        [Display(Name = "Ім'я")]
        public string FirstName { get; set;}

        [Display(Name = "Прізвище")]
        public string LastName { get; set;}

        [Display(Name = "Тип користувача")]
        public UserTypes UserType { get; set;}

        [Display(Name = "Користувача підтверджено")]
        public bool IsConfirmed { get; set;}

    }
}
