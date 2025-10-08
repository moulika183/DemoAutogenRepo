using OrderService.Dtos;

namespace OrderService.Services
{
    public interface IOrderService
    {
        Task<OrderDto> PlaceOrderAsync(PlaceOrderRequestDto request);
        Task<OrderDto?> GetOrderByIdAsync(Guid id);
    }
}
