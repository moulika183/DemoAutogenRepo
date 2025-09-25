```csharp
using OrderService.Dtos;

namespace OrderService.Services;

public interface IOrderService
{
    Task<OrderResponseDto> PlaceOrderAsync(CreateOrderDto dto);
}
```
