
using Microsoft.AspNetCore.Mvc;
using OrderService.Dtos;
using OrderService.Services;

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
    /// Place a new order
    /// </summary>
    /// <param name="dto">Order request DTO</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Order details</returns>
    [HttpPost]
    public async Task<ActionResult<OrderResponseDto>> PlaceOrder(
        [FromBody] PlaceOrderRequestDto dto, 
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _orderService.PlaceOrderAsync(dto, cancellationToken);
        return CreatedAtAction(nameof(PlaceOrder), new { id = result.Id }, result);
    }
}