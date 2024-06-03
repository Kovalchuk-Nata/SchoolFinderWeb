namespace SchoolFinderWeb.Models
{
    public class Article
    {
        public int ArticleID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }

        public byte[]? Photo { get; set; }

    }
}
