```csharp
using Microsoft.AspNetCore.Mvc;
using OrderService.Dtos;
using OrderService.Services;
using Microsoft.Extensions.Logging;

namespace OrderService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _orderService;
    private readonly ILogger<OrdersController> _logger;

    public OrdersController(IOrderService orderService, ILogger<OrdersController> logger)
    {
        _orderService = orderService;
        _logger = logger;
    }

    /// <summary>
    /// Places a new order.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(OrderResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PlaceOrder([FromBody] CreateOrderDto dto)
    {
        try
        {
            var result = await _orderService.PlaceOrderAsync(dto);
            _logger.LogInformation("Order placed: {OrderId}", result.Id);
            return CreatedAtAction(nameof(PlaceOrder), new { id = result.Id }, result);
        }
        catch (ArgumentException ex)
        {
            _logger.LogWarning("Invalid order attempt: {Message}", ex.Message);
            return BadRequest(new { error = ex.Message });
        }
    }
}
```
