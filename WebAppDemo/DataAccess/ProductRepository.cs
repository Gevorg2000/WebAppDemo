using Microsoft.EntityFrameworkCore;
using WebAppDemo.DataAccess.Base;
using WebAppDemo.Models;

namespace WebAppDemo.DataAccess;

public interface IProductRepository : IRepository<Product>
{
    Task<IEnumerable<Product>> SelectPagedProducts(int pageNumber, int size);
}

public class ProductRepository : Repository<Product>, IProductRepository
{
    public ProductRepository(AppDbContext context) 
        : base(context)
    { }

    public async Task<IEnumerable<Product>> SelectPagedProducts(int pageNumber, int size)
    {
        return await Context.Set<Product>()
            .Skip((pageNumber - 1) * size)
            .Take(size)
            .ToListAsync();
    }
}
