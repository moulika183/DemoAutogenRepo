
using OrderService.Dtos;

namespace OrderService.Services;

public interface IOrderService
{
    Task<OrderResponseDto> PlaceOrderAsync(PlaceOrderRequestDto dto, CancellationToken cancellationToken);
}
