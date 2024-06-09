using System.ComponentModel.DataAnnotations;
namespace ShopProject.Models;

public class Field
{
    public int Id { get; set; }
    public string Name { get; set; }
    public  int? CategoryId { get; set; } // Required foreign key property
    public  Category? Category { get; set; }
    public List<FieldValue> FieldValues { get; } = new List<FieldValue>();
}