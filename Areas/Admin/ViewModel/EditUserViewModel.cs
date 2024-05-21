using SchoolFinderWeb.Models;

namespace SchoolFinderWeb.Areas.Admin.ViewModel
{
    public class EditUserViewModel
    {
        public string Id { get; set; }
        public string FirstName { get; set;}
        public string LastName { get; set;}
        public UserTypes UserType { get; set;}

    }
}
