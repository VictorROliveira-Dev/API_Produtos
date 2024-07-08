using APIProdutos.Data.Dtos;
using APIProdutos.Models;
using APIProdutos.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;

namespace APIProdutos.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryController(ICategoryRepository categoryRepository)
    {
         _categoryRepository = categoryRepository;
    }

    [HttpPost]
    public async Task<ActionResult<Category>> AddCategory([FromBody] CategoryDto categoryDto)
    {
        Category category = await _categoryRepository.AddCategory(categoryDto);
        
        return CreatedAtAction(nameof(GetCategoryById), new { id = category.CategoryId }, category);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Category>> GetCategoryById(int id)
    {
        Category? category = await _categoryRepository.GetCategoryById(id);

        if (category == null)
        {
            return NotFound();
        }

        return Ok(category);
    }

    [HttpGet] 
    public async Task<ActionResult<Category>> GetAllCategories()
    {
        List<Category> categories = await _categoryRepository.GetAllCategories();

        return Ok(categories);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Category>> UpdateCategory(int id,[FromBody] CategoryDto categoryDto)
    {
        Category? category = await _categoryRepository.UpdateCategory(id, categoryDto);

        if (category == null)
        {
            return BadRequest();
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteCategory(int id)
    {
        bool category = await _categoryRepository.DeleteCategory(id);

        if (!category)
        {
            return BadRequest();
        }

        return NoContent();
    }
}
