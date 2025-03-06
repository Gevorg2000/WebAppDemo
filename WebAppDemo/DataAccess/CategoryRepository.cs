using Microsoft.EntityFrameworkCore;
using WebAppDemo.DataAccess.Base;
using WebAppDemo.Models;

namespace WebAppDemo.DataAccess;

public interface ICategoryRepository : IRepository<Category>
{
    Task<IEnumerable<Category>> SelectPagedCategories(int pageNumber, int size);
}

public class CategoryRepository : Repository<Category>, ICategoryRepository
{
    public CategoryRepository(AppDbContext context) 
        : base(context)
    { }

    public async Task<IEnumerable<Category>> SelectPagedCategories(int pageNumber, int size)
    {
        return await Context.Set<Category>()
            .Skip((pageNumber - 1) * size)
            .Take(size)
            .ToListAsync();
    }
}
