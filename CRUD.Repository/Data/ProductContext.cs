using CRUD.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CRUD.Data.MySQL.Data
{
    public class ProductContext : DbContext
    {
        public ProductContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }

        public DbSet<Register> Register { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18, 2)");
        }
    }
}
