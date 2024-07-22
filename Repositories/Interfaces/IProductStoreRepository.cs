using APIProdutos.Data.Dtos;
using APIProdutos.Models;

namespace APIProdutos.Repositories.Interfaces;

public interface IProductStoreRepository
{
    Task<ProductStore> AddProductStore(ProductStoreDto productStoreDto);
    Task<List<ProductStore>> GetAllProductStores();
    Task<ProductStore?> GetById(Guid storeId, Guid productId);
    Task<ProductStore?> UpdateProductStore(ProductStoreDto productStoreDto);
    Task<bool> DeleteProductStore(Guid storeId, Guid productId);
}
