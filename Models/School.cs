using System.ComponentModel.DataAnnotations;

namespace SchoolFinderWeb.Models
{
    public class School
    {
        public int SchoolID { get; set; }

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

        [Display(Name = "Група ПД")]
        public bool ExtendedDayGroups { get; set; } // група продовженного дня

        [Display(Name = "Спеціалізація")]
        public bool Specialization { get; set; }

        [Display(Name = "Додаткові освітні можливості")]
        public bool AdditionalOpportunities { get; set; }

        [Required]
        [Display(Name = "Опис")]
        public string Description { get; set; }


        //
        [Required]
        [Display(Name = "Класи")]
        public string Classes { get; set; }

        [Display(Name = "Наявність укриття")]
        public bool Shelter { get; set; }

        [Required]
        [Display(Name = "Формат навчання")]
        public string Format { get; set; }

        [Display(Name = "Участь у всеукраїнських олімпіадах")]
        public bool PaticipationVUO { get; set; }

        [Display(Name = "Школа на карті")]
        public string? Maps { get; set; }
        //



        [Display(Name = "Підтверджено")]
        public bool isConfirmed { get; set; }

        [Display(Name = "Користувач, що додав школу")]
        public string UserId { get; set; }


    }
}
