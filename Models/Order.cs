using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kick_X.Models;

public class Order
{
    [Key]
    public int order_id { get; set;}
    public string? order_uid { get; set;}
    public decimal order_subtotal { get; set;}
    public DateTime order_date { get; set; } = DateTime.Now;
    public string order_status { get; set; } = "Pending"; // Default status

    public int customer_id { get; set; } // FK

    [ForeignKey("customer_id")]
    public Customer? Customer { get; set; }
    
    public ICollection<OrderItem>? OrderItems { get; set; }
}