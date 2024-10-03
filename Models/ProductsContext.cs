using System.Collections.Generic;
using System.Reflection.Emit;

using Microsoft.EntityFrameworkCore;

namespace PruebasAPI.Net.Models;

public class ProductsContext : DbContext
{
    public ProductsContext(DbContextOptions<ProductsContext> options) : base(options)
    {
    }
    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Product>().HasIndex(c => c.Name).IsUnique();
    }
}
