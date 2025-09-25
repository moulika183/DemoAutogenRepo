```csharp
using OrderService.Models;

namespace OrderService.Repositories;

public interface IOrderRepository
{
    Task<Order> CreateAsync(Order order);
}
```
