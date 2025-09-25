using OrderService.Dtos;
using OrderService.Models;
using OrderService.Repositories;

namespace OrderService.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _repo;

        public OrderService(IOrderRepository repo)
        {
            _repo = repo;
        }

        public async Task<OrderDto> PlaceOrderAsync(CreateOrderDto dto)
        {
            var order = new Order
            {
                Id = Guid.NewGuid(),
                CustomerName = dto.CustomerName,
                CreatedAt = DateTime.UtcNow,
                Items = dto.Items.Select(i => new OrderItem
                {
                    ProductId = i.ProductId,
                    Quantity = i.Quantity
                }).ToList()
            };

            await _repo.AddOrderAsync(order);

            return ToDto(order);
        }

        public async Task<OrderDto?> GetOrderByIdAsync(Guid id)
        {
            var order = await _repo.GetOrderByIdAsync(id);
            return order == null ? null : ToDto(order);
        }

        private static OrderDto ToDto(Order order) => new OrderDto
        {
            Id = order.Id,
            CustomerName = order.CustomerName,
            CreatedAt = order.CreatedAt,
            Items = order.Items.Select(it => new OrderItemDto
            {
                ProductId = it.ProductId,
                Quantity = it.Quantity
            }).ToList()
        };
    }
}
