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

    /// <summary>
    /// Cadastrar um prouto e uma loja
    /// </summary>
    /// <param name="productStoreDto">Id do produto e da loja</param>
    /// <returns>Objeto productStore recém-criado</returns>
    /// <response code="201">Created</response>
    /// <response code="400">Bad Request</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ProductStore>> AddProductStore([FromBody] ProductStoreDto productStoreDto)
    {
        ProductStore ProductS = await _productStoreRepository.AddProductStore(productStoreDto);

        ProductStore createdProductStore = await _productStoreRepository.AddProductStore(productStoreDto);
       
        return CreatedAtAction(nameof(GetById), new { storeId = createdProductStore.StoreId, productId = createdProductStore.ProductId }, createdProductStore);
    }

    /// <summary>
    /// Obter dados de um produto e uma loja
    /// </summary>
    /// <param name="storeId">Identificador da loja</param>
    /// <param name="productId">Identificador do produto</param>
    /// <returns>Dados de um produto e uma loja, com ID's especificados</returns>
    /// <response code="200">Success</response>
    /// <response code="404">Not Found</response>
    [HttpGet("{storeId}/{productId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProductStore>> GetById(Guid storeId, Guid productId)
    {
        ProductStore? productStore = await _productStoreRepository.GetById(storeId, productId);

        if (productStore == null)
        {
            return NotFound();
        }

        return Ok(productStore);
    }

    /// <summary>
    /// Obter todos os produtos e lojas
    /// </summary>
    /// <returns>Coleção de produtos e lojas</returns>
    /// <response code="200">Success</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<ProductStore>> GetAllProductStores()
    {
        var productStore = await _productStoreRepository.GetAllProductStores();

        return Ok(productStore);
    }

    /// <summary>
    /// Atualizar dados (ID) de um produto e de uma loja
    /// </summary>
    /// <param name="storeId">Identificador da loja</param>
    /// <param name="productId">Identificador do produto</param>
    /// <returns>Conteúdo vazio</returns>
    /// <response code="204">No Content</response>
    /// <response code="404">Not Found</response>
    /// <response code="400">Bad Request</response>
    [HttpPut("{storeId}/{productId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ProductStore>> UpdateProductStore(Guid storeId, Guid productId,[FromBody] ProductStoreDto productStoreDto)
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
    public async Task<ActionResult> DeleteProductStore(Guid storeId, Guid productId)
    {
        var result = await _productStoreRepository.DeleteProductStore(storeId, productId);
        
        if (!result)
        {
            return NotFound();
        }
        return NoContent();
    }

}
