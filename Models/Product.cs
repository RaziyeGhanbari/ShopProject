using System.ComponentModel.DataAnnotations;

namespace ShopProject.Models;


public class Product
{
    public int Id { get; set; }
    [Display(Name = "نام کالا")]
    public string? Name { get; set; }
    [Display(Name = "قیمت")] 
    public decimal? Price { get; set; }
    [Display(Name = "تصویر")] 
    public string? ImageUrl { get; set; }
  
    [Required]
    [Display(Name = "دسته بندی")] 
    public int CategoryId { get; set; } // Required foreign key property
    [Display(Name = "دسته بندی")] 
    public  Category? Category { get; set; } // Required reference navigation
    public List<FieldValue> FieldValues { get; set; } = [];
}
