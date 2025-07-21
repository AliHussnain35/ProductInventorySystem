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
    [HttpPost("paged")]
public async Task<IActionResult> GetPaged()
{
    // DataTables Parameters
    var draw = HttpContext.Request.Form["draw"].FirstOrDefault();
    var start = Convert.ToInt32(HttpContext.Request.Form["start"].FirstOrDefault() ?? "0");
    var length = Convert.ToInt32(HttpContext.Request.Form["length"].FirstOrDefault() ?? "10");
    var searchValue = HttpContext.Request.Form["search[value]"].FirstOrDefault();

    // Get All Categories
    var query = await _categoryRepository.GetAllAsync();

    // Filtering
    if (!string.IsNullOrWhiteSpace(searchValue))
{
    query = query
        .Where(c => c.CategoryName?.ToLower().Contains(searchValue.ToLower()) == true)
        .ToList();
}

var recordsTotal = query.Count(); // ✅ FIXED


    // Paging
    var data = query
        .Skip(start)
        .Take(length)
        .Select(c => new {
            c.CategoryId,
            c.CategoryName
        })
        .ToList();

    // Return JSON Result in DataTables Format
    return Ok(new {
        draw = draw,
        recordsTotal = recordsTotal,
        recordsFiltered = recordsTotal,
        data = data
    });
}

}
