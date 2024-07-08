using APIProdutos.Data.Dtos;
using APIProdutos.Models;
using APIProdutos.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
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

    [HttpPost]
    public async Task<ActionResult<Store>> CreateStore(StoreDto storeDto)
    {
        Store store = await _storeRepository.CreateStore(storeDto);

        return CreatedAtAction(nameof(GetStoreById), new { id = store.StoreId }, store);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Store>> GetStoreById(int id)
    {
        Store? store = await _storeRepository.GetStoreById(id);

        if (store == null)
        {
            return NotFound();
        }

        return Ok(store);
    }

    [HttpGet]
    public async Task<ActionResult<Store>> GetAllStores()
    {
        List<Store> stores = await _storeRepository.GetAllStores();

        return Ok(stores);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateAsync(int id, StoreDto storeDto)
    {
        Store? store = await _storeRepository.UpdateAsync(id, storeDto);

        if (store == null)
        {
            return BadRequest();
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteStore(int id)
    {
        bool store = await _storeRepository.DeleteStore(id);

        if (!store)
        {
            return BadRequest();
        }

        return NoContent();
    }
}
