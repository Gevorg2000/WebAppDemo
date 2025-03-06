namespace WebAppDemo.DTOs;

public class ProductDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public List<int> CategoryIds { get; set; }
}