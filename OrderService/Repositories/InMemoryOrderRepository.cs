
using OrderService.Models;

namespace OrderService.Repositories;

public class InMemoryOrderRepository : IOrderRepository
{
    private readonly List<Order> _orders = new();
    private readonly object _lock = new();

    public Task<Order> AddAsync(Order order, CancellationToken cancellationToken)
    {
        lock (_lock)
        {
            _orders.Add(order);
        }
        return Task.FromResult(order);
    }
}
