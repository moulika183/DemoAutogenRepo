
using OrderService.Models;

namespace OrderService.Repositories;

public interface IOrderRepository
{
    Task<Order> AddAsync(Order order, CancellationToken cancellationToken);
    // Future: Task<List<Order>> GetAllAsync(CancellationToken cancellationToken);
}
