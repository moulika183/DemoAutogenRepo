namespace OrderService.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public string CustomerName { get; set; } = default!;
        public DateTime OrderDate { get; set; }
        public List<OrderItem> Items { get; set; } = new();
    }
}
