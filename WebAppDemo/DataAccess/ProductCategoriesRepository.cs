using WebAppDemo.DataAccess.Base;
using WebAppDemo.Models;

namespace WebAppDemo.DataAccess;

public interface IProductCategoriesRepository : IRepository<ProductCategory>
{
    IEnumerable<ProductCategory> SelectByProductId(int productId);
    IEnumerable<ProductCategory> SelectByCategoryId(int categoryId);
    void ClearProductCategoriesByProductId(int prodcutId);
}
public class ProductCategoriesRepository : Repository<ProductCategory>, IProductCategoriesRepository
{
    public ProductCategoriesRepository(AppDbContext context)
        : base(context)
    { }

    public IEnumerable<ProductCategory> SelectByCategoryId(int categoryId)
    {
        return Context.Set<ProductCategory>().Where(c => c.CategoryId == categoryId);
    }

    public IEnumerable<ProductCategory> SelectByProductId(int productId)
    {
        return Context.Set<ProductCategory>().Where(c => c.ProductId == productId);
    }

    public void ClearProductCategoriesByProductId(int prodcutId)
    {
        var products = SelectByProductId(prodcutId);
        Context.Set<ProductCategory>().RemoveRange(products);
    }
}
