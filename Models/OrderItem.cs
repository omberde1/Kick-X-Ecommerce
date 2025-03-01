using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kick_X.Models;

public class OrderItem
{
    [Key]
    public int orderitem_id { get; set; }
    public int orderitem_product_quantity { get; set; }

    public int product_id { get; set; } // FK
    public int customer_id { get; set; } // FK
    public int order_id { get; set; } // FK
    
    [ForeignKey("product_id")]
    public Product? Product { get; set; }

    [ForeignKey("customer_id")]
    public Customer? Customer { get; set; }

    [ForeignKey("order_id")]
    public Order? Order { get; set; }

}
