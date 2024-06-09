namespace ShopProject.Models;

public class FieldValuesViewModel
{
    public int CategoryId { get; set; }
    public int ProductId { get; set; }
    public string? ImageUrl { get; set; }
    public List<Field?>? Fields { get; set; }
    public List<FieldValue?>? FieldValues { get; set; }
    
    // public int? idStatus { get; set; }  
}