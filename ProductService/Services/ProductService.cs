using ProductService.Repositories;
using ProductService.Dtos;

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
        var products = await _repository.GetAllAsync();
        return products.Select(p => new ProductDto
        {
            Id = p.Id,
            Name = p.Name,
            Description = p.Description,
            Price = p.Price,
            Category = p.Category
        });
    }

    public async Task<ProductDto?> GetByIdAsync(Guid id)
    {
        var product = await _repository.GetByIdAsync(id);
        if (product == null) return null;
        return new ProductDto
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            Category = product.Category
        };
    }
}
