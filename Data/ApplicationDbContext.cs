using CHARIBOY_ARTS.Areas.Admin.Models;
using CHARIBOY_ARTS.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CHARIBOY_ARTS.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Videos> Videos { get; set; }
        public DbSet<ApplicationUser> applicationUsers { get; set;}
        public DbSet<Comment> comments { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Name = "Tiger",
                    Title = "Love",
                    Category = "Nature",
                    ImageUrl = "",
                    Description = "The best",
                    Price = 40,
                    PriceTotal = 40,
                    PriceDiscount = 0,
                    PriceDiscountTotal = 0,
                   
                });

            modelBuilder.Entity<Videos>().HasData(
                new Videos
                {
                    Id = 2,
                    FileName="",
                    FilePath="",
                    Title="Tiger",
                    Description="Love"
                });
        }
    }
}
