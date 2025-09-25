
```csharp
using OrderService.Dtos;

namespace OrderService.Services
{
    public interface IOrderService
    {
        Task<OrderResponseDto> PlaceOrderAsync(PlaceOrderRequestDto request);
        Task<OrderResponseDto?> GetOrderByIdAsync(Guid orderId);
    }
}
```
