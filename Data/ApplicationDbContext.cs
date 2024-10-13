using CodeVaultApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CodeVaultApi.Data
{
    public class ApplicationDbContext(DbContextOptions dbContextOptions)
        : IdentityDbContext<AppUser>(dbContextOptions)
    {
        public DbSet<Snippet> Snippets { get; set; }

        public DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            List<IdentityRole> roles =
            [
                new IdentityRole { Name = "User", NormalizedName = "USER" },
                new IdentityRole { Name = "Admin", NormalizedName = "ADMIN" },
            ];
            modelBuilder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
