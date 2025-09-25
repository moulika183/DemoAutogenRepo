
```csharp
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OrderService.Dtos;
using OrderService.Services;

namespace OrderService.Controllers
{
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

        // POST api/orders
        [HttpPost]
        public async Task<ActionResult<OrderResponseDto>> PlaceOrder([FromBody] PlaceOrderRequestDto request)
        {
            _logger.LogInformation("Received place order request for customer {CustomerName}", request.CustomerName);
            var order = await _orderService.PlaceOrderAsync(request);
            return CreatedAtAction(nameof(GetOrderById), new { id = order.Id }, order);
        }

        // GET api/orders/{id}
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<OrderResponseDto>> GetOrderById([FromRoute] Guid id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return order;
        }
    }
}
```
