namespace SchoolFinderWeb.Models
{
    public class Article
    {
        public int ArticleID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }

        public byte[]? Photo { get; set; }
    }
}
