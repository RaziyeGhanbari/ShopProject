namespace ShopProject.Models;

public class EditProductViewModel
{
    public int Id { get; set; }
    
    public string? Name { get; set; }
    
    public decimal? Price { get; set; }
    
    public string? ImageUrl { get; set; }
    
    public int CategoryId { get; set; } // Required foreign key property
    
    public List<FieldValue?>? FieldValues { get; set; }
}