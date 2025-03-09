using Microsoft.EntityFrameworkCore;
using CafeMenuProject.Models;


namespace CafeMenuProject.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Category> CATEGORY { get; set; }
        public DbSet<Product> PRODUCT { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<ProductProperty> ProductProperties { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
