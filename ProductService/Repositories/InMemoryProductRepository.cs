```csharp
using ProductService.Models;

namespace ProductService.Repositories;

public class InMemoryProductRepository : IProductRepository
{
    private readonly List<Product> _products = new();

    public Task<IEnumerable<Product>> GetAllAsync()
    {
        return Task.FromResult<IEnumerable<Product>>(_products);
    }

    public Task<Product?> GetByIdAsync(Guid id)
    {
        var product = _products.FirstOrDefault(x => x.Id == id);
        return Task.FromResult(product);
    }

    public Task AddAsync(Product product)
    {
        _products.Add(product);
        return Task.CompletedTask;
    }

    public Task UpdateAsync(Product product)
    {
        var idx = _products.FindIndex(x => x.Id == product.Id);
        if (idx >= 0)
        {
            _products[idx] = product;
        }
        return Task.CompletedTask;
    }

    public Task<bool> DeleteAsync(Guid id)
    {
        var idx = _products.FindIndex(x => x.Id == id);
        if (idx < 0) return Task.FromResult(false);
        _products.RemoveAt(idx);
        return Task.FromResult(true);
    }
}
```
