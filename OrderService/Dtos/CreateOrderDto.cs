```csharp
namespace OrderService.Dtos;

public class CreateOrderDto
{
    public string CustomerName { get; set; } = string.Empty;
    public List<OrderItemDto> Items { get; set; } = new();
}

public class OrderItemDto
{
    public string ProductId { get; set; } = string.Empty;
    public int Quantity { get; set; }
}
```
