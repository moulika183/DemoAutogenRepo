using OrderManagement.Models;
using System.Collections.Concurrent;

namespace OrderManagement.Repositories
{
    public class InMemoryOrderRepository : IOrderRepository
    {
        private readonly ConcurrentDictionary<Guid, Order> _orders = new();

        public Task AddAsync(Order order)
        {
            _orders[order.Id] = order;
            return Task.CompletedTask;
        }

        public Task<Order?> GetByIdAsync(Guid id)
        {
            _orders.TryGetValue(id, out var order);
            return Task.FromResult(order);
        }
    }
}
