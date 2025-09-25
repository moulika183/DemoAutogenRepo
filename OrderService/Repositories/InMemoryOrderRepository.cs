```csharp
using OrderService.Models;

namespace OrderService.Repositories;

public class InMemoryOrderRepository : IOrderRepository
{
    private static readonly List<Order> Orders = new();

    public Task<Order> CreateAsync(Order order)
    {
        order.Id = Guid.NewGuid();
        order.CreatedAt = DateTime.UtcNow;
        Orders.Add(order);
        return Task.FromResult(order);
    }
}
```
