
```csharp
using OrderService.Models;

namespace OrderService.Repositories
{
    public class InMemoryOrderRepository : IOrderRepository
    {
        private static readonly List<Order> _orders = new();
        private static readonly SemaphoreSlim _lock = new(1, 1);

        public async Task AddOrderAsync(Order order)
        {
            await _lock.WaitAsync();
            try
            {
                _orders.Add(order);
            }
            finally
            {
                _lock.Release();
            }
        }

        public async Task<Order?> GetOrderByIdAsync(Guid orderId)
        {
            await Task.Yield();
            return _orders.FirstOrDefault(o => o.Id == orderId);
        }
    }
}
```
