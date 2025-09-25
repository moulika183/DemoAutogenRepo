using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OrderManagement.Dtos
{
    public class CreateOrderDto
    {
        [Required]
        public string CustomerName { get; set; } = string.Empty;

        [Required]
        [MinLength(1)]
        public List<CreateOrderItemDto> Items { get; set; } = new List<CreateOrderItemDto>();
    }

    public class CreateOrderItemDto
    {
        [Required]
        public string ProductId { get; set; } = string.Empty;

        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }
    }
}
