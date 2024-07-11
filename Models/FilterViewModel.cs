namespace ShopProject.Models;

public class FilterViewModel
{
    public string Name { get; set; }
    
    public string Id { get; set; }
    public List<Product>? Products { get; set; }
    
    public int? SearchCategoryId { get; set; }
    
    // public string? SearchFieldValue { get; set; }
    
    public int? SubCategoryId { get; set; }

    public List<FieldValueFilter>? FieldValueFilters { get; set; } = new();
}