
namespace OrderService.Models;

public class Order
{
    public Guid Id { get; set; }
    public string CustomerName { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public List<OrderItem> Items { get; set; } = new();
    public decimal TotalAmount { get; set; }
}

