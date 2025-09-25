
```csharp
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

        public async Task<OrderResponseDto> PlaceOrderAsync(PlaceOrderRequestDto request)
        {
            var order = new Order
            {
                CustomerName = request.CustomerName,
                Items = request.Items.Select(i => new OrderItem
                {
                    ProductId = i.ProductId,
                    ProductName = i.ProductName,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice
                }).ToList(),
                CreatedAt = DateTime.UtcNow
            };

            await _orderRepository.AddOrderAsync(order);
            return MapToDto(order);
        }

        public async Task<OrderResponseDto?> GetOrderByIdAsync(Guid orderId)
        {
            var order = await _orderRepository.GetOrderByIdAsync(orderId);
            if (order == null) return null;
            return MapToDto(order);
        }

        private static OrderResponseDto MapToDto(Order order) =>
            new OrderResponseDto
            {
                Id = order.Id,
                CustomerName = order.CustomerName,
                CreatedAt = order.CreatedAt,
                Items = order.Items.Select(i => new OrderItemDto
                {
                    ProductId = i.ProductId,
                    ProductName = i.ProductName,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice
                }).ToList(),
                TotalAmount = order.TotalAmount
            };
    }
}
```
