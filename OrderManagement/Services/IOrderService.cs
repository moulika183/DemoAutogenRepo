using OrderManagement.Dtos;

namespace OrderManagement.Services
{
    public interface IOrderService
    {
        Task<OrderDto?> PlaceOrderAsync(CreateOrderDto dto);
        Task<OrderDto?> GetOrderByIdAsync(Guid id);
    }
}
