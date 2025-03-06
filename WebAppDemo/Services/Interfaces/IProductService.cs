using WebAppDemo.DTOs;

namespace WebAppDemo.Services.Interfaces;

public interface IProductService
{
    Task<IEnumerable<ProductDTO>> GetAllProductsAsync();
    Task<ProductDTO> GetProductByIdAsync(int id);
    Task CreateProductAsync(ProductDTO productDTO);
    Task UpdateProductAsync(int id, ProductDTO productDTO);
    Task DeleteProductAsync(int id);
}
