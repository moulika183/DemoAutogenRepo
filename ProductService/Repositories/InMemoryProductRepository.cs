```csharp
using ProductService.Models;

namespace ProductService.Repositories;

public class InMemoryProductRepository : IProductRepository
{
    private readonly Dictionary<Guid, Product> _products = new();

    public Task<IEnumerable<Product>> GetAllAsync()
        => Task.FromResult(_products.Values.AsEnumerable());

    public Task<Product?> GetByIdAsync(Guid id)
        => Task.FromResult(_products.TryGetValue(id, out var product) ? product : null);

    public Task<Product> AddAsync(Product product)
    {
        product.Id = Guid.NewGuid();
        _products[product.Id] = product;
        return Task.FromResult(product);
    }

    public Task<bool> UpdateAsync(Product product)
    {
        if (!_products.ContainsKey(product.Id))
            return Task.FromResult(false);

        _products[product.Id] = product;
        return Task.FromResult(true);
    }

    public Task<bool> DeleteAsync(Guid id)
    {
        if (!_products.ContainsKey(id))
            return Task.FromResult(false);

        _products.Remove(id);
        return Task.FromResult(true);
    }
}
```
