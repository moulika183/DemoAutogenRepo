
```csharp
namespace OrderService.Dtos
{
    public class OrderResponseDto
    {
        public Guid Id { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public List<OrderItemDto> Items { get; set; } = new();
        public DateTime CreatedAt { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
```
