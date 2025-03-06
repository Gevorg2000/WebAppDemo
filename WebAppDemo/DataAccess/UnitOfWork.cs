using WebAppDemo.DataAccess.Base;
using WebAppDemo.Models;

namespace WebAppDemo.DataAccess;

public interface IUnitOfWork : IDisposable
{
    IProductRepository Products { get; set; }
    ICategoryRepository Categories { get; set; }
    IProductCategoriesRepository ProductCategories { get; set; }
    Task CommitAsync();
}

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    public UnitOfWork(AppDbContext context)
    {
        _context = context;
        Products = new ProductRepository(_context);
        Categories = new CategoryRepository(_context);
        ProductCategories = new ProductCategoriesRepository(_context);
    }

    public IProductRepository Products { get; set; }
    public ICategoryRepository Categories { get; set; }
    public IProductCategoriesRepository ProductCategories { get; set; }

    public async Task CommitAsync()
    {
        await _context.SaveChangesAsync();
    }

    public void Commit()
    {
        _context.SaveChanges();
    }

    public void Dispose() 
    { 
        _context.Dispose();
    }
}
