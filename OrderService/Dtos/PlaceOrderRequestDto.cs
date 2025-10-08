
using System.ComponentModel.DataAnnotations;

namespace OrderService.Dtos;

public class PlaceOrderRequestDto
{
    [Required]
    public string CustomerName { get; set; } = null!;

    [Required]
    [MinLength(1)]
    public List<OrderItemDto> Items { get; set; } = null!;
}

public class OrderItemDto
{
    [Required]
    public string ProductName { get; set; } = null!;

    [Range(1, int.MaxValue)]
    public int Quantity { get; set; }

    [Range(0, double.MaxValue)]
    public decimal UnitPrice { get; set; }
}

