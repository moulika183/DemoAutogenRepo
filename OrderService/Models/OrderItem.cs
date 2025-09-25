
```csharp
namespace OrderService.Models
{
    public class OrderItem
    {
        public string ProductId { get; set; } = string.Empty;
        public string? ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
```
