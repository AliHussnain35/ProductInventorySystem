using Microsoft.EntityFrameworkCore;
using ProductInventorySystem.Data;
using ProductInventorySystem.Models;

namespace ProductInventorySystem.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly InventoryContext _context;

    public ProductRepository(InventoryContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await _context.Products.Include(p => p.Category).ToListAsync();
    }

    public async Task<Product?> GetByIdAsync(int id)
    {
        return await _context.Products.Include(p => p.Category)
            .FirstOrDefaultAsync(p => p.ProductId == id);
    }

    public async Task AddAsync(Product product)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Product product)
    {
        _context.Products.Update(product);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product != null)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }
    }
    //public async Task<List<Product>> GetAllWithCategoryAsync()
    //{
    //    return await _context.Products.Include(p => p.Category).ToListAsync();
    //}
    public async Task<IEnumerable<object>> GetAllWithCategoryAsync()
    {
        return await _context.Products
            .Include(p => p.Category)
            .Select(p => new
            {
                p.ProductId,
                p.ProductName,
                p.Price,
                CategoryName = p.Category.CategoryName
            })
            .ToListAsync();
    }
}
