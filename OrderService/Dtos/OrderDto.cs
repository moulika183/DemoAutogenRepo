using System;

namespace OrderService.Dtos
{
    public class OrderDto
    {
        public Guid Id { get; set; }
        public string CustomerName { get; set; } = default!;
        public DateTime OrderDate { get; set; }
        public List<OrderItemDto> Items { get; set; } = new();
    }

    public class OrderItemDto
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
