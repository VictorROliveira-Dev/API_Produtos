using APIProdutos.Data;
using APIProdutos.Data.Dtos;
using APIProdutos.Models;
using APIProdutos.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace APIProdutos.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _appDbContext;

    public ProductRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<Product> AddProduct(ProductDto productDto)
    {
        string filePath = Path.Combine("storage", productDto.Image.FileName);

        using (FileStream stream = new FileStream(filePath, FileMode.Create))
        {
            await productDto.Image.CopyToAsync(stream);
        }

        Product product = new Product
        {
            Name = productDto.Name,
            Description = productDto.Description,
            Amount = productDto.Amount,
            Price = productDto.Price,
            PathImage = filePath,
            CategoryId = productDto.CategoryId,
        };

        await _appDbContext.Products.AddAsync(product);
        await _appDbContext.SaveChangesAsync();
        
        return product;
    }

    public async Task<bool> DeleteProduct(Guid id)
    {
        Product? product = _appDbContext.Products.FirstOrDefault(p => p.ProductId == id);
        
        if (product == null)
        {
            throw new ArgumentException($"Produto com id: {id} não encontrado.");
        }

        _appDbContext.Products.Remove(product);
        await _appDbContext.SaveChangesAsync();

        return true;
    }

    public async Task<List<Product>> GetAllProducts()
    {
        return await _appDbContext.Products.Include(p => p.Category).ToListAsync();
    }

    public async Task<Product?> GetProductById(Guid id)
    {
        return await _appDbContext.Products.Include(p => p.Category).FirstOrDefaultAsync(prod => prod.ProductId == id);
    }

    public async Task<Product?> UpdateProduct(Guid id, ProductDto productDto)
    {
        var product = await _appDbContext.Products.FindAsync(id);

        if (product == null)
        {
            return null;
        }

        product.Name = productDto.Name;
        product.Description = productDto.Description;
        product.Amount = productDto.Amount;
        product.Price = productDto.Price;
        product.CategoryId = productDto.CategoryId;

        if (productDto.Image != null)
        {
            var filePath = Path.Combine("storage", productDto.Image.FileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await productDto.Image.CopyToAsync(stream);
            }

            product.PathImage = filePath;
        }

        _appDbContext.Products.Update(product);
        await _appDbContext.SaveChangesAsync();

        return product;
    }
}
