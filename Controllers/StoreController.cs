using APIProdutos.Data.Dtos;
using APIProdutos.Models;
using APIProdutos.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace APIProdutos.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StoreController : ControllerBase
{
    private readonly IStoreRepository _storeRepository;

    public StoreController(IStoreRepository storeRepository)
    {
        _storeRepository = storeRepository;
    }

    /// <summary>
    /// Cadastrar uma loja
    /// </summary>
    /// <param name="storeDto">Dados da loja</param>
    /// <returns>Objeto loja recém-criado</returns>
    /// <response code="201">Created</response>
    /// <response code="400">Bad Request</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Store>> CreateStore(StoreDto storeDto)
    {
        Store store = await _storeRepository.CreateStore(storeDto);

        return CreatedAtAction(nameof(GetStoreById), new { id = store.StoreId }, store);
    }

    /// <summary>
    /// Obter dados de uma loja
    /// </summary>
    /// <param name="id">Identificador da loja</param>
    /// <returns>Dados de uma loja com ID especificado</returns>
    /// <response code="200">Success</response>
    /// <response code="404">Not Found</response>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Store>> GetStoreById(Guid id)
    {
        Store? store = await _storeRepository.GetStoreById(id);

        if (store == null)
        {
            return NotFound();
        }

        return Ok(store);
    }

    /// <summary>
    /// Obter todos as lojas
    /// </summary>
    /// <returns>Coleção de lojas</returns>
    /// <response code="200">Success</response>
    [HttpGet]
    /// <response code="200">Success</response>
    public async Task<ActionResult<Store>> GetAllStores()
    {
        List<Store> stores = await _storeRepository.GetAllStores();

        return Ok(stores);
    }

    /// <summary>
    /// Atualizar dados de uma loja
    /// </summary>
    /// <param name="id">Identificador da loja</param>
    /// <returns>Conteúdo vazio</returns>
    /// <response code="204">No Content</response>
    /// <response code="404">Not Found</response>
    /// <response code="400">Bad Request</response>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> UpdateAsync(Guid id, StoreDto storeDto)
    {
        Store? store = await _storeRepository.UpdateAsync(id, storeDto);

        if (store == null)
        {
            return BadRequest();
        }

        return NoContent();
    }

    /// <summary>
    /// Remover uma loja
    /// </summary>
    /// <param name="id">Identificador da loja</param>
    /// <returns>Conteudo vazio</returns>
    /// <response code="204">No Content</response>
    /// <response code="400">Bad Request</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> DeleteStore(Guid id)
    {
        bool store = await _storeRepository.DeleteStore(id);

        if (!store)
        {
            return BadRequest();
        }

        return NoContent();
    }
}
