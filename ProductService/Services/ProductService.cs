```csharp
using ProductService.Dtos;
using ProductService.Models;
using ProductService.Repositories;

namespace ProductService.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _repo;

    public ProductService(IProductRepository repo)
    {
        _repo = repo;
    }

    public async Task<IEnumerable<ProductDto>> GetAllAsync()
    {
        var products = await _repo.GetAllAsync();
        return products.Select(ToDto);
    }

    public async Task<ProductDto?> GetByIdAsync(Guid id)
    {
        var product = await _repo.GetByIdAsync(id);
        return product is null ? null : ToDto(product);
    }

    public async Task<ProductDto> AddAsync(CreateProductDto dto)
    {
        var product = new Product
        {
            Name = dto.Name,
            Description = dto.Description,
            Price = dto.Price,
            Category = dto.Category
        };
        product = await _repo.AddAsync(product);
        return ToDto(product);
    }

    public async Task<bool> UpdateAsync(Guid id, UpdateProductDto dto)
    {
        var existing = await _repo.GetByIdAsync(id);
        if (existing is null)
            return false;

        existing.Name = dto.Name;
        existing.Description = dto.Description;
        existing.Price = dto.Price;
        existing.Category = dto.Category;
        return await _repo.UpdateAsync(existing);
    }

    public async Task<bool> DeleteAsync(Guid id)
        => await _repo.DeleteAsync(id);

    private static ProductDto ToDto(Product product)
        => new ProductDto
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            Category = product.Category
        };
}
```
