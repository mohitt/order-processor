using Microsoft.EntityFrameworkCore;

namespace OrderProcessor
{
    public interface IDBContext
    {
        
        DbSet<Order> Orders { get; set; }
        DbSet<Product> Products { get; set; }

        int SaveChanges();
    }
}