
using WebApp.Models;

namespace WebApp.ViewModels;

public class OrderCreateViewModel
{
    public int CustomerId { get; set; }
    public List<OrderItemInput> Items { get; set; } = new();
    public List<Customer> Customers { get; set; } = new();
    public List<Product> Products { get; set; } = new();
    public decimal Total { get; set; }
}

public class OrderItemInput
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}
