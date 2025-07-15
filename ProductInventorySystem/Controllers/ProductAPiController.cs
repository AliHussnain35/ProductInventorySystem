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
    }
}
