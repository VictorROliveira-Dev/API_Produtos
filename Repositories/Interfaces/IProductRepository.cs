using APIProdutos.Data.Dtos;
using APIProdutos.Models;

namespace APIProdutos.Repositories.Interfaces;

public interface IProductRepository
{
    Task<Product> AddProduct(ProductDto productDto);
    Task<List<Product>> GetAllProducts(); 
    Task<Product?> GetProductById(Guid id); 
    Task<Product?> UpdateProduct(Guid id, ProductDto productDto);
    Task<bool> DeleteProduct(Guid id);
}
