using Microsoft.EntityFrameworkCore;
using RESTful_MS.Models;

namespace RESTful_MS.Data
{
    public class MarketContext : DbContext
    {
        public MarketContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<Brand> Brands { get; set; } = null!;
    }
}
