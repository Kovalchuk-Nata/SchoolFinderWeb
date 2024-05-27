using SchoolFinderWeb.Models;

namespace SchoolFinderWeb.ViewModels
{
    internal class SchoolProfileViewModel
    {
        public School School { get; set; }
        public int LikesCount { get; set; }
        public int DislikesCount { get; set; }
    }
}