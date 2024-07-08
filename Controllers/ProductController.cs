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

    [HttpPost]
    public async Task<ActionResult<Product>> AddProduct([FromForm] ProductDto productDto)
    {
        Product product = await _productRepository.AddProduct(productDto);
        return CreatedAtAction(nameof(GetProductById), new { id = product.ProductId }, product);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProductById(int id)
    {
        Product product = await _productRepository.GetProductById(id);

        if (product == null)
        {
            return NotFound();
        }

        return Ok(product);
    }

    [HttpGet]
    public async Task<ActionResult<Product>> GetAllProducts()
    {
        List<Product> products = await _productRepository.GetAllProducts();

        return Ok(products);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateProduct(int id,[FromForm] ProductDto productDto)
    {
        var product = await _productRepository.UpdateProduct(id, productDto);

        if (product == null)
        {
            return BadRequest();
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteProduct(int id)
    {
        var result = await _productRepository.DeleteProduct(id);

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
