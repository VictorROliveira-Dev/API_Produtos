using APIProdutos.Data;
using APIProdutos.Data.Dtos;
using APIProdutos.Models;
using APIProdutos.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace APIProdutos.Repositories;

public class StoreRepository : IStoreRepository
{
    private readonly AppDbContext _appDbContext;

    public StoreRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<Store> CreateStore(StoreDto storeDto)
    {
        Store store = new Store
        {
            StoreName = storeDto.StoreName,
            Address = storeDto.Address,
        };

        await _appDbContext.Stores.AddAsync(store);
        await _appDbContext.SaveChangesAsync();

        return store;
    }

    public async Task<bool> DeleteStore(Guid id)
    {
        Store? store = await _appDbContext.Stores.FirstOrDefaultAsync(s => s.StoreId == id);

        if (store == null)
        {
            throw new ArgumentException($"Loja com id: {id} não foi encontrada.");
        }

        _appDbContext.Stores.Remove(store);
        await _appDbContext.SaveChangesAsync();
        
        return true;
    }

    public async Task<List<Store>> GetAllStores()
    {
        return await _appDbContext.Stores.ToListAsync();
    }

    public async Task<Store?> GetStoreById(Guid id)
    {
        return await _appDbContext.Stores.FirstOrDefaultAsync(s => s.StoreId == id);  
    }

    public async Task<Store?> UpdateAsync(Guid id, StoreDto storeDto)
    {
        Store? store = await _appDbContext.Stores.FirstOrDefaultAsync(s => s.StoreId == id);

        if (store == null)
        {
            throw new ArgumentException($"Loja com id: {id} não foi encontrada.");
        }

        store.StoreName = storeDto.StoreName;
        store.Address = storeDto.Address;

        _appDbContext.Update(store);
        await _appDbContext.SaveChangesAsync();
        
        return store;
    }
}
