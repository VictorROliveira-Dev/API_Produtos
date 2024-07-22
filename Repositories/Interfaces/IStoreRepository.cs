using APIProdutos.Data.Dtos;
using APIProdutos.Models;

namespace APIProdutos.Repositories.Interfaces;

public interface IStoreRepository
{
    Task<Store> CreateStore(StoreDto storeDto);
    Task<List<Store>> GetAllStores();
    Task<Store?> GetStoreById(Guid id);
    Task<Store?> UpdateAsync(Guid id, StoreDto storeDto);
    Task<bool> DeleteStore(Guid id);
}
