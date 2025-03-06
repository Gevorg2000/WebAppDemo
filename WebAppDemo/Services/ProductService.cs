using WebAppDemo.DataAccess;
using WebAppDemo.DTOs;
using WebAppDemo.Models;
using WebAppDemo.Services.Interfaces;

namespace WebAppDemo.Services;

public class ProductService : IProductService
{
    private readonly IUnitOfWork _unitOfWork;
    public ProductService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task CreateProductAsync(ProductDTO productDTO)
    {
        Product newProduct = new Product()
        {
            Name = productDTO.Name,
            Price = productDTO.Price
        };

        await _unitOfWork.Products.Insert(newProduct);
        await _unitOfWork.CommitAsync();

        foreach (int categoryId in productDTO.CategoryIds)
        {
            var pc = new ProductCategory() 
            { 
                ProductId = newProduct.Id,
                CategoryId = categoryId
            };
            await _unitOfWork.ProductCategories.Insert(pc);
        }
        await _unitOfWork.CommitAsync();
    }

    public async Task UpdateProductAsync(int id, ProductDTO productDTO)
    {
        Product product = await _unitOfWork.Products.SelectById(id);

        product.Name = productDTO.Name;
        product.Price = productDTO.Price;

        _unitOfWork.ProductCategories.ClearProductCategoriesByProductId(id);

        foreach (int categoryId in productDTO.CategoryIds)
        {
            var pc = new ProductCategory()
            {
                ProductId = product.Id,
                CategoryId = categoryId
            };
            await _unitOfWork.ProductCategories.Insert(pc);
        }

        await _unitOfWork.CommitAsync();
    }

    public async Task DeleteProductAsync(int id)
    {
        await _unitOfWork.Products.Delete(id);
        await _unitOfWork.CommitAsync();
    }

    public async Task<IEnumerable<ProductDTO>> GetAllProductsAsync()
    {
        var allproducts = await _unitOfWork.Products.SelectAll();
        var productDtos = new List<ProductDTO>();
        foreach (Product product in allproducts)
        {
            productDtos.Add(new ProductDTO()
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                CategoryIds = product.Categories.Select(c => c.Id).ToList()
            });
        }

        return productDtos;
    }

    public async Task<ProductDTO> GetProductByIdAsync(int id)
    {
        Product product = await _unitOfWork.Products.SelectById(id);
        return new ProductDTO
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price,
            CategoryIds = product.Categories.Select(c => c.Id).ToList()
        };
    }
}
