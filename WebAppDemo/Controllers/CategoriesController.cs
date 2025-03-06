using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAppDemo.DTOs;
using WebAppDemo.Services.Interfaces;

namespace WebAppDemo.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<IActionResult> GetCategories()
    {
        var categories = await _categoryService.GetAllCategoriesAsync();
        return Ok(categories);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCategory(int id)
    {
        var category = await _categoryService.GetCategoryByIdAsync(id);
        if (category is null)
            return NotFound();
        return Ok(category);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCategory([FromBody] CategoryDTO categoryDTO)
    {
        await _categoryService.CreateCategoryAsync(categoryDTO);
        return CreatedAtAction(nameof(GetCategory), new { id = categoryDTO.Id }, categoryDTO);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCategory(int id, [FromBody] CategoryDTO categoryDTO)
    {
        await _categoryService.UpdateCategoryAsync(id, categoryDTO);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        await _categoryService.DeleteCategoryAsync(id);
        return NoContent();
    }
}
