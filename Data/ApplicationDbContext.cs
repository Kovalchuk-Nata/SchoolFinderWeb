using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SchoolFinderWeb.Models;

namespace SchoolFinderWeb.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)  { }

        public DbSet<School> School { get; set; }
        public DbSet<Article> Article { get; set; }
    }
}