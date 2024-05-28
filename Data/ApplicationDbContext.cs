using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SchoolFinderWeb.Models;

namespace SchoolFinderWeb.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<School> School { get; set; }
        public DbSet<Article> Article { get; set; }
        public DbSet<FavoriteSchool> FavoriteSchools { get; set; }
        public DbSet<Compare> Compare { get; set; }
        public DbSet<Likes> Likes { get; set; }
        public DbSet<VUO> VUO { get; set; }

    }
}