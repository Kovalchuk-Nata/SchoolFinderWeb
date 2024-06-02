using SchoolFinderWeb.Models;

namespace SchoolFinderWeb.ViewModels
{
    public class FindSchoolViewModel
    {
        public List<string> Districts { get; set; } = new List<string>();
        public List<string> SelectedDistricts { get; set; } = new List<string>();
        public List<string> Types { get; set; } = new List<string>();
        public List<string> SelectedTypes { get; set; } = new List<string>();
        public List<string> Classes { get; set; } = new List<string>();
        public List<string> SelectedClasses { get; set; } = new List<string>();
        public List<string> Format { get; set; } = new List<string>();
        public List<string> SelectedFormat { get; set; } = new List<string>();

        public decimal? MaxPrice { get; set; }
        public bool? IsExtendedDayGroups { get; set; }
        public bool? IsSpecialization { get; set; }
        public bool? IsAdditionalOpportunities { get; set; }
        public bool? IsShelter { get; set; }
        public bool? IsPaticipationVUO { get; set; }


        public List<School> Schools { get; set; } = new();
    }
}
