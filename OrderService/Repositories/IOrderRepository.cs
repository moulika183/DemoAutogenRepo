
```csharp
using OrderService.Models;

namespace OrderService.Repositories
{
    public interface IOrderRepository
    {
        Task AddOrderAsync(Order order);
        Task<Order?> GetOrderByIdAsync(Guid orderId);
    }
}
```
