namespace SchoolFinderWeb.Models
{
    public class Likes
    {
        public int Id { get; set; }
        public string UserID { get; set; }
        public User? User { get; set; } // Зв'язок з користувачем

        public int SchoolID { get; set; }
        public School? School { get; set; } // Зв'язок зі школою

        public bool IsLike { get; set; } // Поле для зберігання лайка або дизлайка (true для лайка, false для дизлайка) 

    }
}
