using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace SchoolFinderWeb.Models
{
    public enum UserTypes
    {
        [Display(Name = "Parent")]
        Parent = 0,

        [Display(Name = "School organizator")]
        Organizator = 1,

        [Display(Name = "Admin")]
        Admin = 2
    }
}
