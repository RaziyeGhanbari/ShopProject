using System.ComponentModel.DataAnnotations;

namespace ShopProject.Models;


public class Product
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public decimal? Price { get; set; }
    [Display(Name = "Image")] 
    public string? ImageUrl { get; set; }
  
    [Required]
    [Display(Name = "Category")] 
    public int CategoryId { get; set; } // Required foreign key property
    public  Category? Category { get; set; } // Required reference navigation
    public List<FieldValue> FieldValues { get; set; } = [];
}
