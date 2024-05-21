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

        [Display(Name = "Підтверджено")]
        public bool isConfirmed { get; set; }

        [Display(Name = "Користувач, що додав школу")]
        public string UserId { get; set; }


    }
}
        /*
        SchoolID: це буде унікальний ідентифікатор, який слід назначити кожній школі. Це може бути, наприклад, унікальний номер або рядок.
        Назва: назва школи, яка буде відображатися на сайті.
        Адреса: адреса розташування школи, щоб користувачі могли легко знайти її.
        Район міста: цей атрибут може бути перелічувальним типом з варіантами "Богунський" та "Корольовський".
        Тип: тип школи, який може бути визначений як "приватна" або "державна".
        Ціна навчання: вартість навчання в даній школі.
        Групи продовженого дня: значення true/false, щоб показати наявність груп продовженого дня.
        Поглиблений напрямок навчання: значення true/false, щоб вказати, чи існує в школі поглиблений напрямок навчання.
        Додаткові освітні можливості: це також значення true/false, щоб показати, чи є в школі додаткові освітні можливості, такі як спорт, басейн, мистецтво, мови або наукові клуби.
        Загальний опис школи: це поле може бути текстовим полем, де можна розмістити багато інформації про школу, її програми, підходи до навчання тощо.
        Фото: можливо, вам знадобиться поле для завантаження фотографій школи.
         */
