```csharp
using System.Collections.Concurrent;
using ProductService.Models;

namespace ProductService.Repositories;

public class ProductRepository : IProductRepository
{
    private static readonly ConcurrentDictionary<Guid, Product> Products = new ConcurrentDictionary<Guid, Product>();

    public Task<IEnumerable<Product>> GetAllAsync()
    {
        return Task.FromResult<IEnumerable<Product>>(Products.Values.ToList());
    }

    public Task<Product?> GetByIdAsync(Guid id)
    {
        Products.TryGetValue(id, out var prod);
        return Task.FromResult(prod);
    }

    public Task AddAsync(Product product)
    {
        Products[product.Id] = product;
        return Task.CompletedTask;
    }

    public Task UpdateAsync(Product product)
    {
        Products[product.Id] = product;
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Guid id)
    {
        Products.TryRemove(id, out var _);
        return Task.CompletedTask;
    }
}
```
