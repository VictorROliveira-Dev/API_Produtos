using APIProdutos.Data.Dtos;
using APIProdutos.Models;
using APIProdutos.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace APIProdutos.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductStoreController : ControllerBase
{
    private readonly IProductStoreRepository _productStoreRepository;

    public ProductStoreController(IProductStoreRepository productStoreRepository)
    {
        _productStoreRepository = productStoreRepository;
    }

    [HttpPost]
    public async Task<ActionResult<ProductStore>> AddProductStore([FromBody] ProductStoreDto productStoreDto)
    {
        ProductStore ProductS = await _productStoreRepository.AddProductStore(productStoreDto);

        ProductStore createdProductStore = await _productStoreRepository.AddProductStore(productStoreDto);
       
        return CreatedAtAction(nameof(GetById), new { storeId = createdProductStore.StoreId, productId = createdProductStore.ProductId }, createdProductStore);
    }

    [HttpGet("{storeId}/{productId}")]
    public async Task<ActionResult<ProductStore>> GetById(int storeId, int productId)
    {
        ProductStore? productStore = await _productStoreRepository.GetById(storeId, productId);

        if (productStore == null)
        {
            return NotFound();
        }

        return Ok(productStore);
    }

    [HttpGet]
    public async Task<ActionResult<ProductStore>> GetAllProductStores()
    {
        var productStore = await _productStoreRepository.GetAllProductStores();

        return Ok(productStore);
    }

    [HttpPut("{storeId}/{productId}")]
    public async Task<ActionResult<ProductStore>> UpdateProductStore(int storeId, int productId,[FromBody] ProductStoreDto productStoreDto)
    {
        if (storeId != productStoreDto.StoreId || productId != productStoreDto.ProductId)
        {
            return BadRequest();
        }

        var updatedProductStore = await _productStoreRepository.UpdateProductStore(productStoreDto);
        
        if (updatedProductStore == null)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{storeId}/{productId}")]
    public async Task<ActionResult> DeleteProductStore(int storeId, int productId)
    {
        var result = await _productStoreRepository.DeleteProductStore(storeId, productId);
        
        if (!result)
        {
            return NotFound();
        }
        return NoContent();
    }

}
