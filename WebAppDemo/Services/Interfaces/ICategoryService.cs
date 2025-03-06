using WebAppDemo.DTOs;

namespace WebAppDemo.Services.Interfaces;

public interface ICategoryService
{
    Task<IEnumerable<CategoryDTO>> GetAllCategoriesAsync();
    Task<CategoryDTO> GetCategoryByIdAsync(int id);
    Task CreateCategoryAsync(CategoryDTO categoryDTO);
    Task UpdateCategoryAsync(int id, CategoryDTO categoryDTO);
    Task DeleteCategoryAsync(int id);
}
