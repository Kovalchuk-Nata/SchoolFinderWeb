using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolFinderWeb.Models
{
    public class User : IdentityUser
    {
        [PersonalData]
        [Column(TypeName = "nvarchar(100)")]
        public string FirstName { get; set; }

        [PersonalData]
        [Column(TypeName = "nvarchar(100)")]
        public string LastName { get; set; }
        public bool IsConfirmed { get; set; }

        public UserTypes UserType { get; set; }

        // Зв'язок з обраними школами
        public List<FavoriteSchool>? FavoriteSchools { get; set; }
        public List<Article>? Articles { get; set; }

    }
}
