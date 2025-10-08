using ProductService.Repositories;
using ProductService.Dtos;

namespace ProductService.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repo;

        public ProductService(IProductRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
            var entities = await _repo.GetAllAsync();
            return entities.Select(e => new ProductDto
            {
                Id = e.Id,
                Name = e.Name,
                Description = e.Description,
                Price = e.Price,
                Category = e.Category
            });
        }

        public async Task<ProductDto?> GetProductByIdAsync(Guid id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null) return null;

            return new ProductDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                Price = entity.Price,
                Category = entity.Category
            };
        }
    }
}
