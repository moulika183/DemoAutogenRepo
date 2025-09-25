using ProductCatalogManagement.Models;

namespace ProductCatalogManagement.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product?> GetByIdAsync(Guid id);
        Task AddAsync(Product product);
        Task UpdateAsync(Product product);
        Task<bool> DeleteAsync(Guid id);
    }
}
