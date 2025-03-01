using Kick_X.Models;

namespace Kick_X.ViewModels.DashboardViewModel;

public class DashboardViewModel
{
    public List<Customer>? Customers { get; set; }
    public List<Order>? Orders { get; set; }
}
