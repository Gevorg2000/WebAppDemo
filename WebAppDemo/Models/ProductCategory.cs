using WebAppDemo.Models.Base;

namespace WebAppDemo.Models;

public class ProductCategory : BaseEntity
{
    public int ProductId { get; set; }
    public int CategoryId { get; set; }
}
