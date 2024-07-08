using APIProdutos.Data.Dtos;
using APIProdutos.Models;

namespace APIProdutos.Repositories.Interfaces;

public interface IProductRepository
{
    Task<Product> AddProduct(ProductDto productDto);
    Task<List<Product>> GetAllProducts(); 
    Task<Product> GetProductById(int id); 
    Task<Product> UpdateProduct(int id, ProductDto productDto);
    Task<bool> DeleteProduct(int id);
}
