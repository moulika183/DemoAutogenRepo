
namespace OrderService.Dtos;

public class OrderResponseDto
{
    public Guid Id { get; set; }
    public string CustomerName { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public List<OrderItemResponseDto> Items { get; set; } = new();
    public decimal TotalAmount { get; set; }
}

public class OrderItemResponseDto
{
    public string ProductName { get; set; } = null!;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}
