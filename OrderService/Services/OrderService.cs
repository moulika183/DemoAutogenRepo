using OrderService.Dtos;
using OrderService.Models;
using OrderService.Repositories;

namespace OrderService.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<OrderDto> PlaceOrderAsync(PlaceOrderRequestDto request)
        {
            var order = new Order
            {
                Id = Guid.NewGuid(),
                CustomerName = request.CustomerName,
                OrderDate = DateTime.UtcNow,
                Items = request.Items.Select(i => new OrderItem
                {
                    ProductId = i.ProductId,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice
                }).ToList()
            };

            await _orderRepository.AddAsync(order);

            return new OrderDto
            {
                Id = order.Id,
                CustomerName = order.CustomerName,
                OrderDate = order.OrderDate,
                Items = order.Items.Select(x => new OrderItemDto
                {
                    ProductId = x.ProductId,
                    Quantity = x.Quantity,
                    UnitPrice = x.UnitPrice
                }).ToList()
            };
        }

        public async Task<OrderDto?> GetOrderByIdAsync(Guid id)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            if (order == null) return null;

            return new OrderDto
            {
                Id = order.Id,
                CustomerName = order.CustomerName,
                OrderDate = order.OrderDate,
                Items = order.Items.Select(x => new OrderItemDto
                {
                    ProductId = x.ProductId,
                    Quantity = x.Quantity,
                    UnitPrice = x.UnitPrice
                }).ToList()
            };
        }
    }
}
