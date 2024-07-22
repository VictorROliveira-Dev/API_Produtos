using APIProdutos.Data;
using APIProdutos.Data.Dtos;
using APIProdutos.Models;
using APIProdutos.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace APIProdutos.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly AppDbContext _appDbContext;

    public CategoryRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<Category> AddCategory(CategoryDto categoryDto)
    {
        Category category = new Category
        {
            CategoryName = categoryDto.CategoryName,
            Description = categoryDto.Description,
        };

        await _appDbContext.Categories.AddAsync(category);
        await _appDbContext.SaveChangesAsync();

        return category;
    }

    public async Task<bool> DeleteCategory(int id)
    {
        Category? category = await _appDbContext.Categories.FirstOrDefaultAsync(c => c.CategoryId == id);

        if (category == null)
        {
            throw new ArgumentException($"Categoria com id: {id} não encontrado.");
        }

        _appDbContext.Categories.Remove(category);
        await _appDbContext.SaveChangesAsync();

        return true;
    }

    public async Task<List<Category>> GetAllCategories()
    {
        return await _appDbContext.Categories.Include(c => c.Products).ToListAsync();
    }

    public async Task<Category?> GetCategoryById(int id)
    {
        return await _appDbContext.Categories.FirstOrDefaultAsync(c => c.CategoryId == id);
    }

    public async Task<Category?> UpdateCategory(int id, CategoryDto categoryDto)
    {
        Category? category = await _appDbContext.Categories.FirstOrDefaultAsync(c => c.CategoryId == id);

        if (category == null)
        {
            throw new ArgumentException($"Categoria com id: {id} não encontrado.");
        }

        category.CategoryName = categoryDto.CategoryName;
        category.Description = categoryDto.Description;

        _appDbContext.Categories.Update(category);
        await _appDbContext.SaveChangesAsync();

        return category;
    }
}
