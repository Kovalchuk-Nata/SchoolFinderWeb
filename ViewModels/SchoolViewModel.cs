using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace SchoolFinderWeb.ViewModels
{
    public class SchoolViewModel
    {
        [Required]
        [Display(Name = "Назва")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Адреса")]
        public string Address { get; set; } // адреса школи

        [Required]
        [Display(Name = "Район")]
        public string District { get; set; } // район богунський/корольовський

        [Required]
        [Display(Name = "Тип")]
        public string Type { get; set; } // тип школи державна/приватна

        [Display(Name = "Вартість")]
        public uint? Price { get; set; } // вартість навчання за рік

        [Display(Name = "Група продовженого дня")]
        public bool ExtendedDayGroups { get; set; } // група продовженного дня

        [Display(Name = "Поглиблений напрям навчання (гуманітарний, математичний тощо)")]
        public bool Specialization { get; set; }

        [Display(Name = "Додаткові освітні можливості (спорт і тд)")]
        public bool AdditionalOpportunities { get; set; }

        [Required]
        [Display(Name = "Опис")]
        public string Description { get; set; }
    }
}
