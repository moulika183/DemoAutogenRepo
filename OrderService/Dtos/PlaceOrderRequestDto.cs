using System.ComponentModel.DataAnnotations;

namespace OrderService.Dtos
{
    public class PlaceOrderRequestDto
    {
        [Required]
        public string CustomerName { get; set; } = default!;

        [Required]
        [MinLength(1)]
        public List<PlaceOrderItemDto> Items { get; set; } = new();
    }

    public class PlaceOrderItemDto
    {
        [Required]
        public Guid ProductId { get; set; }

        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }

        [Range(0, double.MaxValue)]
        public decimal UnitPrice { get; set; }
    }
}
