using System.ComponentModel.DataAnnotations;
namespace ShopProject.Models;

public class FieldValue
{
    public int Id { get; set; }
    public string? Value { get; set; }
    public int? ProductId { get; set; }
    public int FieldId { get; set; }
    
    public  Product? Product { get; set; }
    
    public  Field? Field { get; set; }
}