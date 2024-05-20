using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace SchoolFinderWeb.Models
{
   // [Keyless]
    public class FavoriteSchool
    {
        [Key]
        public int Id { get; set; }

        public string UserID { get; set; }
        public User? User { get; set; } // Зв'язок з користувачем

        public int SchoolID { get; set; }
        public School? School { get; set; } // Зв'язок зі школою
    }
}
