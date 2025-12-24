using ChineseAction.Api.Model;
using ChineseAction.Api.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpPost]
    public async Task<ActionResult<Category>> AddCategory([FromBody] Category category)
    {
        var addedCategory = await _categoryService.AddCategoryAsync(category);
        return CreatedAtAction(nameof(GetCategoryById), new { id = addedCategory.Id }, addedCategory);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Category>> GetCategoryById(int id)
    {
        var category = await _categoryService.GetCategoryByIdAsync(id);
        if (category == null)
        {
            return NotFound();
        }
        return Ok(category);
    }
}