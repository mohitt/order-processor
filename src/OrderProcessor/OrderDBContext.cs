using Microsoft.EntityFrameworkCore;

namespace OrderProcessor
{
    public class OrderDBContext : DbContext, IDBContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=orders.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(new Product() {Name = "FreshBook Software Lic", Quantity = 10, Price = 3});
        }
        
        
    }
}