using System.ComponentModel.DataAnnotations;
namespace ShopProject.Models;

public class Field
{
    public int Id { get; set; }
    [Display(Name = "Field")]
    public string Name { get; set; }
    public  int? CategoryId { get; set; } // Required foreign key property
    public  Category? Category { get; set; }
    
    public List<FieldValue> FieldValues { get; } = new();
    
    public bool IsDeleted { get; set; }
}