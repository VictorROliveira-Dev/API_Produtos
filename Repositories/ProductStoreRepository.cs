using APIProdutos.Data;
using APIProdutos.Data.Dtos;
using APIProdutos.Models;
using APIProdutos.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Xml.Serialization;

namespace APIProdutos.Repositories;

public class ProductStoreRepository : IProductStoreRepository
{
    private readonly AppDbContext _appDbContext;

    public ProductStoreRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<ProductStore> AddProductStore(ProductStoreDto productStoreDto)
    {
        ProductStore ps = new ProductStore
        {
            StoreId = productStoreDto.StoreId,
            ProductId = productStoreDto.ProductId,
        };

        _appDbContext.ProductStores.Add(ps);
        await _appDbContext.SaveChangesAsync();

        return ps;
    }

    public async Task<bool> DeleteProductStore(int storeId, int productId)
    {
        var productStore = await GetById(storeId, productId);

        if (productStore == null)
        {
            return false;
        }

        _appDbContext.ProductStores.Remove(productStore);
        await _appDbContext.SaveChangesAsync();

        return true;
    }

    public async Task<List<ProductStore>> GetAllProductStores()
    {
        return await _appDbContext.ProductStores.Include(ps => ps.Store).Include(ps => ps.Product).ToListAsync();
    }

    public async Task<ProductStore?> GetById(int storeId, int productId)
    {
        return await _appDbContext.ProductStores
                                  .Include(ps => ps.Store)
                                  .Include(ps => ps.Product)
                                  .FirstOrDefaultAsync(ps => ps.StoreId == storeId && ps.ProductId == productId);
    }

    public async Task<ProductStore?> UpdateProductStore(ProductStoreDto productStoreDto)
    {
        var productStore = await GetById(productStoreDto.StoreId, productStoreDto.ProductId);

        if (productStore == null)
        {
            return null;
        }

        productStore.StoreId = productStoreDto.StoreId;
        productStore.ProductId = productStoreDto.ProductId;

        _appDbContext.ProductStores.Update(productStore);
        await _appDbContext.SaveChangesAsync();

        return productStore;
    }
}
