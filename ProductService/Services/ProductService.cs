```csharp
using ProductService.Dtos;
using ProductService.Models;
using ProductService.Repositories;

namespace ProductService.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _repository;

    public ProductService(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<ProductDto>> GetAllAsync()
    {
        var items = await _repository.GetAllAsync();
        return items.Select(MapToDto);
    }

    public async Task<ProductDto?> GetByIdAsync(Guid id)
    {
        var prod = await _repository.GetByIdAsync(id);
        return prod == null ? null : MapToDto(prod);
    }

    public async Task<ProductDto> CreateAsync(CreateProductDto dto)
    {
        var prod = new Product
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            Description = dto.Description,
            Price = dto.Price,
            SKU = dto.SKU
        };
        await _repository.AddAsync(prod);
        return MapToDto(prod);
    }

    public async Task<ProductDto?> UpdateAsync(Guid id, UpdateProductDto dto)
    {
        var existing = await _repository.GetByIdAsync(id);
        if (existing == null)
            return null;

        existing.Name = dto.Name;
        existing.Description = dto.Description;
        existing.Price = dto.Price;
        existing.SKU = dto.SKU;

        await _repository.UpdateAsync(existing);

        return MapToDto(existing);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var product = await _repository.GetByIdAsync(id);
        if (product == null)
            return false;

        await _repository.DeleteAsync(id);
        return true;
    }

    private static ProductDto MapToDto(Product product) =>
        new ProductDto
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            SKU = product.SKU
        };
}
```
