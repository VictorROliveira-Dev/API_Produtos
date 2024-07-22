using APIProdutos.Data.Dtos;
using APIProdutos.Models;
using APIProdutos.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace APIProdutos.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductRepository _productRepository;

    public ProductController(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    /// <summary>
    /// Cadastrar um produto
    /// </summary>
    /// <param name="productDto">Dados do produto</param>
    /// <returns>Objeto produto recém-criado</returns>
    /// <response code="201">Created</response>
    /// <response code="400">Bad Request</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Product>> AddProduct([FromForm] ProductDto productDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        Product product = await _productRepository.AddProduct(productDto);
        return CreatedAtAction(nameof(GetProductById), new { id = product.ProductId }, product);
    }

    /// <summary>
    /// Obter dados de um produto
    /// </summary>
    /// <param name="id">Identificador do produto</param>
    /// <returns>Dados de um produto com ID especificado</returns>
    /// <response code="200">Success</response>
    /// <response code="404">Not Found</response>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Product>> GetProductById(Guid id)
    {
        Product? product = await _productRepository.GetProductById(id);

        if (product == null)
        {
            return NotFound();
        }

        return Ok(product);
    }

    /// <summary>
    /// Obter todos os produtos
    /// </summary>
    /// <returns>Coleção de produtos</returns>
    /// <response code="200">Success</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<Product>> GetAllProducts()
    {
        List<Product> products = await _productRepository.GetAllProducts();

        return Ok(products);
    }

    /// <summary>
    /// Atualizar dados de um produto
    /// </summary>
    /// <param name="id">Identificador do produto</param>
    /// <returns>Conteúdo vazio</returns>
    /// <response code="204">No Content</response>
    /// <response code="404">Not Found</response>
    /// <response code="400">Bad Request</response>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> UpdateProduct(Guid id,[FromForm] ProductDto productDto)
    {
        var product = await _productRepository.UpdateProduct(id, productDto);

        if (product == null)
        {
            return BadRequest();
        }

        if (product.ProductId !=  id)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Remover um produto
    /// </summary>
    /// <param name="id">Identificador do produto</param>
    /// <returns>Conteudo vazio</returns>
    /// <response code="204">No Content</response>
    /// <response code="400">Bad Request</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> DeleteProduct(Guid id)
    {
        var result = await _productRepository.DeleteProduct(id);

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
