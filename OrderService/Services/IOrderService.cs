using OrderService.Dtos;

namespace OrderService.Services
{
    public interface IOrderService
    {
        Task<OrderDto> PlaceOrderAsync(CreateOrderDto dto);
        Task<OrderDto?> GetOrderByIdAsync(Guid id);
    }
}
