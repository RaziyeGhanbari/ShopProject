namespace ShopProject.Models;

public class FilterViewModel
{
    public List<Product>? Products { get; set; }
    
    public string? SearchCategory { get; set; }
    
    public string? SearchFieldValue { get; set; }
}