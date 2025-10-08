using ProductService.Models;

namespace ProductService.Repositories;

public class InMemoryProductRepository : IProductRepository
{
    private readonly List<Product> _products;

    public InMemoryProductRepository()
    {
        _products = new List<Product>
        {
            new Product { Id = Guid.NewGuid(), Name = "Book", Description = "A great book", Price = 10.99M, Category = "Books" },
            new Product { Id = Guid.NewGuid(), Name = "Laptop", Description = "A powerful laptop", Price = 1500.00M, Category = "Electronics" },
            new Product { Id = Guid.NewGuid(), Name = "Headphones", Description = "Noise-cancelling", Price = 200.00M, Category = "Electronics" }
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
