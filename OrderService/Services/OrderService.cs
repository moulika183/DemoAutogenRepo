```csharp
using OrderService.Models;
using OrderService.Dtos;
using OrderService.Repositories;

namespace OrderService.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _repo;

    public OrderService(IOrderRepository repo)
    {
        _repo = repo;
    }

    public async Task<OrderResponseDto> PlaceOrderAsync(CreateOrderDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.CustomerName) || dto.Items is null || !dto.Items.Any())
            throw new ArgumentException("Invalid order data.");

        var order = new Order
        {
            CustomerName = dto.CustomerName,
            Items = dto.Items.Select(i => new OrderItem
            {
                ProductId = i.ProductId,
                Quantity = i.Quantity
            }).ToList()
        };

        var created = await _repo.CreateAsync(order);

        return new OrderResponseDto
        {
            Id = created.Id,
            CreatedAt = created.CreatedAt,
            CustomerName = created.CustomerName,
            Items = created.Items.Select(x => new OrderItemDto
            {
                ProductId = x.ProductId,
                Quantity = x.Quantity
            }).ToList()
        };
    }
}
```
