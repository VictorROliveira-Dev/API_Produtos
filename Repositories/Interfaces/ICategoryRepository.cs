using APIProdutos.Data.Dtos;
using APIProdutos.Models;

namespace APIProdutos.Repositories.Interfaces;

public interface ICategoryRepository
{
    Task<Category> AddCategory(CategoryDto categoryDto);
    Task<bool> DeleteCategory(int id);
    Task<List<Category>> GetAllCategories();
    Task<Category?> GetCategoryById(int id);
    Task<Category?> UpdateCategory(int id, CategoryDto categoryDto);
}
