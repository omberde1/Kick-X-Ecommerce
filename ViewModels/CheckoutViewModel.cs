using Kick_X.Models;

namespace Kick_X.ViewModels;

public class CheckoutViewModel
{
    public Customer? Customer { get; set; }
    public List<CartItem>? CartItems { get; set; } = new List<CartItem>();
}
