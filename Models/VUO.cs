using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace SchoolFinderWeb.Models
{
    public class VUO
    {
        public int Id { get; set; }
        
        [Required]
        [Display(Name = "ПІБ учня")]
        public string Student { get; set; }

        [Required]
        [Display(Name = "Повна назва школи")]
        public string SchoolName { get; set; }

        [Required]
        [Display(Name = "Клас, в якому учень навчається")]
        public int Class { get; set; }

        [Display(Name = "Призове місце")]
        public int? DiplomPlace { get; set; }

        [Required]
        [Display(Name = "Предмет")]
        public string Subject { get; set; }

        [Required]
        [Display(Name = "Рік олімпіади")]
        public int OlimpiadYear { get; set; }

        [Display(Name = "ID школи")]
        public int? SchoolId { get; set; }
        public School? School { get; set;} // зв'язок зі школою
    }
}
