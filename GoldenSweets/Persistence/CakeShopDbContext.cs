using GoldenSweets.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GoldenSweets.Persistence
{
    public class GoldenSweetsDbContext : IdentityDbContext<IdentityUser>
    {
        public DbSet<Cake> Cakes { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Gallery> Galleries { get; set; }
        public DbSet<GalleryImage> GalleryImages { get; set; }

        public GoldenSweetsDbContext(DbContextOptions<GoldenSweetsDbContext> options)
            : base(options)
        {

        }
    }
}
