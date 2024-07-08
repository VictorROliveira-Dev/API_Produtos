using APIProdutos.Data.Dtos;
using APIProdutos.Models;

namespace APIProdutos.Repositories.Interfaces;

public interface IStoreRepository
{
    Task<Store> CreateStore(StoreDto storeDto);
    Task<List<Store>> GetAllStores();
    Task<Store?> GetStoreById(int id);
    Task<Store?> UpdateAsync(int id, StoreDto storeDto);
    Task<bool> DeleteStore(int id);
}
