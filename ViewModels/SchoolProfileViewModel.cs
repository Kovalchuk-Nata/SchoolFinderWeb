using SchoolFinderWeb.Models;

namespace SchoolFinderWeb.ViewModels
{
    public class SchoolProfileViewModel
    {
        public School School { get; set; }
        public int LikesCount { get; set; }
        public int DislikesCount { get; set; }
        public List<OlympiadDataByYear> OlympiadDataByYear { get; set; }
        public List<OlympiadDataBySubject> OlympiadDataBySubject { get; set; }
    }

    public class OlympiadDataByYear
    {
        public int Year { get; set; }
        public int Count { get; set; }
    }

    public class OlympiadDataBySubject
    {
        public int Year { get; set; }
        public string Subject { get; set; }
        public int Count { get; set; }
    }
}