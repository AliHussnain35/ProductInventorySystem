using Microsoft.EntityFrameworkCore;
using ProductInventorySystem.Models;

namespace ProductInventorySystem.Data;

public class InventoryContext : DbContext
{
    public InventoryContext(DbContextOptions<InventoryContext> options) : base(options) { }

    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
}
