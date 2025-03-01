using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kick_X.Models;

public class CartItem
{
    [Key]
    public int cartitem_id { get; set;}
    public int cartitem_product_quantity { get; set; }

    public int product_id { get; set; } // FK
    public int customer_id { get; set; } // FK
    
    [ForeignKey("product_id")]
    public Product? Product { get; set; }

    [ForeignKey("customer_id")]
    public Customer? Customer { get; set; }
}