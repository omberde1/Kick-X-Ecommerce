using System.ComponentModel.DataAnnotations;

namespace Kick_X.Models;
public class Category
{
    [Key]
    public int category_id { get; set;}
    public string? category_name { get; set;}

    public ICollection<Product>? Product { get; set; }
}