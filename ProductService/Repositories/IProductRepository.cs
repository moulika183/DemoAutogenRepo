```csharp
using ProductService.Models;

namespace ProductService.Repositories;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllAsync();
    Task<Product?> GetByIdAsync(Guid id);
    Task<Product> AddAsync(Product product);
    Task<bool> UpdateAsync(Product product);
    Task<bool> DeleteAsync(Guid id);
}
```
