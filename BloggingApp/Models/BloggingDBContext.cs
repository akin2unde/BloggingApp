using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BloggingApp.Models
{
    public class BloggingDBContext: IdentityDbContext<AppUser>
    {
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public BloggingDBContext()
        { }
        public BloggingDBContext(DbContextOptions<BloggingDBContext> options)
            : base(options)
        { }
        public virtual DbSet<Blog> Blogs { get; set; }
        public virtual DbSet<AppUser> AppUsers { get; set; }
    }
}
