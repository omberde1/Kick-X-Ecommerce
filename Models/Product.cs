using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Kick_X.Models;
public class Product
{
    [Key]
    public int product_id { get; set;}
    public string? product_name { get; set;}
    public decimal product_price { get; set;}
    public string? product_description { get; set;}
    public string? product_image { get; set;}
    public int category_id { get; set; } // FK

    [ForeignKey("category_id")]
    public Category? Category { get; set; }
}
