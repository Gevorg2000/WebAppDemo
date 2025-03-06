using WebAppDemo.Models.Base;

namespace WebAppDemo.Models;

public class Product : BaseEntity
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public List<Category> Categories { get; set; }
}
