using WebAppDemo.DataAccess;
using WebAppDemo.DTOs;
using WebAppDemo.Models;
using WebAppDemo.Services.Interfaces;

namespace WebAppDemo.Services;

public class CategoryService : ICategoryService
{
    private readonly IUnitOfWork _unitOfWork;
    public CategoryService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task CreateCategoryAsync(CategoryDTO categoryDTO)
    {
        Category newCategory = new Category()
        {
            Name = categoryDTO.Name,
        };

        await _unitOfWork.Categories.Insert(newCategory);
        await _unitOfWork.CommitAsync();
    }

    public async Task UpdateCategoryAsync(int id, CategoryDTO categoryDTO)
    {
        Category category = await _unitOfWork.Categories.SelectById(id);
        category.Name = categoryDTO.Name;
        await _unitOfWork.CommitAsync();
    }

    public async Task DeleteCategoryAsync(int id)
    {
        await _unitOfWork.Categories.Delete(id);
        await _unitOfWork.CommitAsync();
    }

    public async Task<IEnumerable<CategoryDTO>> GetAllCategoriesAsync()
    {
        var allcategories = await _unitOfWork.Categories.SelectAll();
        var categoryDtos = new List<CategoryDTO>();
        foreach (var item in allcategories)
        {
            var categoryDto = new CategoryDTO()
            {
                Name = item.Name
            };

            categoryDtos.Add(categoryDto);
        }

        return categoryDtos;
    }

    public async Task<CategoryDTO> GetCategoryByIdAsync(int id)
    {
        Category category = await _unitOfWork.Categories.SelectById(id);
        return new CategoryDTO
        {
            Id = category.Id,
            Name = category.Name
        };
    }
}
