using Microsoft.AspNetCore.Mvc;
using ProductInventorySystem.Models;
using ProductInventorySystem.Repositories;

namespace ProductInventorySystem.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoryApiController : ControllerBase
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryApiController(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Category>>> GetAll()
    {
        var categories = await _categoryRepository.GetAllAsync();
        return Ok(categories);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Category>> GetById(int id)
    {
        var category = await _categoryRepository.GetByIdAsync(id);
        if (category == null)
            return NotFound();
        return Ok(category);
    }

    [HttpPost]
    public async Task<ActionResult> Create(Category category)
    {
        await _categoryRepository.AddAsync(category);
        return CreatedAtAction(nameof(GetById), new { id = category.CategoryId }, category);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, Category category)
    {
        if (id != category.CategoryId)
            return BadRequest();

        await _categoryRepository.UpdateAsync(category);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await _categoryRepository.DeleteAsync(id);
        return NoContent();
    }
}
