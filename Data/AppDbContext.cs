using APIProdutos.Models;
using Microsoft.EntityFrameworkCore;

namespace APIProdutos.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Store> Stores { get; set; }
    public DbSet<ProductStore> ProductStores { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ProductStore>()
                    .HasKey(ps => new { ps.StoreId, ps.ProductId });

        modelBuilder.Entity<ProductStore>()
                    .HasOne(ps => ps.Store)
                    .WithMany(s => s.ProductStores)
                    .HasForeignKey(ps => ps.StoreId);

        modelBuilder.Entity<ProductStore>()
                    .HasOne(ps => ps.Product)
                    .WithMany(p => p.ProductStores)
                    .HasForeignKey(ps => ps.ProductId);
    }
}
