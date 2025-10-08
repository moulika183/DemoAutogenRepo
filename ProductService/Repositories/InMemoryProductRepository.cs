using ProductService.Models;

namespace ProductService.Repositories
{
    public class InMemoryProductRepository : IProductRepository
    {
        private readonly List<Product> _products;

        public InMemoryProductRepository()
        {
            // Seed with some in-memory data
            _products = new List<Product>
            {
                new Product
                {
                    Id = Guid.NewGuid(), Name = "Coffee Mug", Description = "A ceramic mug.", Price = 7.99M, Category = "Kitchenware", CreatedAt = DateTime.UtcNow.AddDays(-2)
                },
                new Product
                {
                    Id = Guid.NewGuid(), Name = "Notebook", Description = "A ruled notebook.", Price = 2.49M, Category = "Stationery", CreatedAt = DateTime.UtcNow.AddDays(-10)
                },
                new Product
                {
                    Id = Guid.NewGuid(), Name = "Bluetooth Speaker", Description = "Wireless speaker.", Price = 49.99M, Category = "Electronics", CreatedAt = DateTime.UtcNow.AddDays(-5)
                },
            };
        }

        public Task<IEnumerable<Product>> GetAllAsync()
        {
            return Task.FromResult(_products.AsEnumerable());
        }

        public Task<Product?> GetByIdAsync(Guid id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            return Task.FromResult(product);
        }
    }
}
