using Microsoft.AspNetCore.Mvc;
using OrderManagement.Dtos;
using OrderManagement.Models;
using OrderManagement.Services;

namespace OrderManagement.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly ILogger<OrdersController> _logger;

        public OrdersController(IOrderService orderService, ILogger<OrdersController> logger)
        {
            _orderService = orderService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult<OrderDto>> PlaceOrder([FromBody] CreateOrderDto dto)
        {
            _logger.LogInformation("Placing a new order {@Order}", dto);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var order = await _orderService.PlaceOrderAsync(dto);
            if (order == null)
                return BadRequest("Unable to place order.");

            return CreatedAtAction(nameof(GetOrderById), new { id = order.Id }, order);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDto>> GetOrderById(Guid id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null)
                return NotFound();

            return Ok(order);
        }
    }
}
