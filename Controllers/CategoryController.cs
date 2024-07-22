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

    /// <summary>
    /// Cadastrar uma categoria
    /// </summary>
    /// <param name="categoryDto">Dados da categoria</param>
    /// <returns>Objeto categoria recém-criado</returns>
    /// <response code="201">Created</response>
    /// <response code="400">Bad Request</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Category>> AddCategory([FromBody] CategoryDto categoryDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        Category category = await _categoryRepository.AddCategory(categoryDto);
        
        return CreatedAtAction(nameof(GetCategoryById), new { id = category.CategoryId }, category);
    }

    /// <summary>
    /// Buscar uma categoria por ID
    /// </summary>
    /// <param name="id">Identificador da categoria</param>
    /// <returns>Dados de uma categoria com ID específico</returns>
    /// <response code="200">Success</response>
    /// <response code="404">Not Found</response>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Category>> GetCategoryById(int id)
    {
        Category? category = await _categoryRepository.GetCategoryById(id);

        if (category == null)
        {
            return NotFound();
        }

        return Ok(category);
    }

    /// <summary>
    /// Obter todas as categorias
    /// </summary>
    /// <returns>Coleção de categorias</returns>
    /// <response code="200">Success</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<Category>> GetAllCategories()
    {
        List<Category> categories = await _categoryRepository.GetAllCategories();

        return Ok(categories);
    }

    /// <summary>
    /// Atualizar uma categoria
    /// </summary>
    /// <param name="id">Identificador da categoria</param>
    /// <returns>Conteudo vazio</returns>
    /// <response code="204">No Content</response>
    /// <response code="400">Bad Request</response>
    /// <response code="404">Not Found</response>
    /// <response code="400">Bad Request</response>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Category>> UpdateCategory(int id,[FromBody] CategoryDto categoryDto)
    {
        Category? category = await _categoryRepository.UpdateCategory(id, categoryDto);

        if (category == null)
        {
            return BadRequest();
        }

        if (category.CategoryId != id)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Remover uma categoria
    /// </summary>
    /// <param name="id">Identificador da categoria</param>
    /// <returns>Conteudo vazio</returns>
    /// <response code="204">No Content</response>
    /// <response code="400">Bad Request</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
