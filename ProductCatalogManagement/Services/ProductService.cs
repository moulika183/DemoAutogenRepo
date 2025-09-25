using ProductCatalogManagement.Dtos;
using ProductCatalogManagement.Models;
using ProductCatalogManagement.Repositories;

namespace ProductCatalogManagement.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repo;
        private readonly ILogger<ProductService> _logger;

        public ProductService(IProductRepository repo, ILogger<ProductService> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        public async Task<IEnumerable<ProductDto>> GetAllAsync()
        {
            var products = await _repo.GetAllAsync();
            return products.Select(MapToDto);
        }

        public async Task<ProductDto?> GetByIdAsync(Guid id)
        {
            var product = await _repo.GetByIdAsync(id);
            return product == null ? null : MapToDto(product);
        }

        public async Task<ProductDto> CreateAsync(CreateProductDto dto)
        {
            var product = new Product
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                Category = dto.Category
            };
            await _repo.AddAsync(product);
            _logger.LogInformation("Product {ProductId} created", product.Id);
            return MapToDto(product);
        }

        public async Task<ProductDto?> UpdateAsync(Guid id, UpdateProductDto dto)
        {
            var product = await _repo.GetByIdAsync(id);
            if (product == null) return null;

            product.Name = dto.Name;
            product.Description = dto.Description;
            product.Price = dto.Price;
            product.Category = dto.Category;
            await _repo.UpdateAsync(product);
            _logger.LogInformation("Product {ProductId} updated", product.Id);
            return MapToDto(product);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var deleted = await _repo.DeleteAsync(id);
            if (deleted)
                _logger.LogInformation("Product {ProductId} deleted", id);
            return deleted;
        }

        private static ProductDto MapToDto(Product p) => new()
        {
            Id = p.Id,
            Name = p.Name,
            Description = p.Description,
            Price = p.Price,
            Category = p.Category
        };
    }
}
