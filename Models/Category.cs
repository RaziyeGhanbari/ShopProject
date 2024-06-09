using System.ComponentModel.DataAnnotations;
namespace ShopProject.Models;

public class Category
{
    public int Id { get; set; }
    
    [StringLength(30)]
    public string? Name { get; set; }
    public int? ParentId { get; set; } = null!;
    [Display(Name = "Parent")]
    public Category? Parent {get; set;} = default!;
    public ICollection<Product> Products { get; } = new List<Product>();
    public ICollection<Field> Fields { get; set; } = new List<Field>();
}