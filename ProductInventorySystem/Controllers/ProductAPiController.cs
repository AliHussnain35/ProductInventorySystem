using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductInventorySystem.Models;
using ProductInventorySystem.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductInventorySystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductApiController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductApiController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAll()
        {
            var products = await _productRepository.GetAllAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetById(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
                return NotFound();
            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult> Create(Product product)
        {
            await _productRepository.AddAsync(product);
            return CreatedAtAction(nameof(GetById), new { id = product.ProductId }, product);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, Product product)
        {
            if (id != product.ProductId)
                return BadRequest();

            await _productRepository.UpdateAsync(product);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _productRepository.DeleteAsync(id);
            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> GetPagedProducts()
        {
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = Convert.ToInt32(Request.Form["start"].FirstOrDefault());
            var length = Convert.ToInt32(Request.Form["length"].FirstOrDefault());
            var searchValue = Request.Form["search[value]"].FirstOrDefault()?.ToLower();

            var allProducts = await _productRepository.GetAllWithCategoryAsync();

            // Convert to strongly-typed list (assumes anonymous objects with required fields)
            var products = allProducts
                .Select(p => new
                {
                    ProductId = (int)p.GetType().GetProperty("ProductId")!.GetValue(p)!,
                    ProductName = p.GetType().GetProperty("ProductName")!.GetValue(p)!.ToString()!,
                    Price = (decimal)p.GetType().GetProperty("Price")!.GetValue(p)!,
                    CategoryName = p.GetType().GetProperty("CategoryName")?.GetValue(p)?.ToString() ?? ""
                })
                .ToList();

            // Search filter
            if (!string.IsNullOrEmpty(searchValue))
            {
                products = products.Where(p =>
                    p.ProductName.ToLower().Contains(searchValue) ||
                    p.CategoryName.ToLower().Contains(searchValue)
                ).ToList();
            }

            var totalRecords = products.Count;

            var pagedData = products
                .Skip(start)
                .Take(length)
                .ToList();

            return Ok(new
            {
                draw = draw,
                recordsTotal = totalRecords,
                recordsFiltered = totalRecords,
                data = pagedData
            });
        }
    }
}
