using ProductCatalogManagement.Models;
using System.Collections.Concurrent;

namespace ProductCatalogManagement.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ConcurrentDictionary<Guid, Product> _products = new();

        public Task<IEnumerable<Product>> GetAllAsync()
            => Task.FromResult(_products.Values.AsEnumerable());

        public Task<Product?> GetByIdAsync(Guid id)
            => Task.FromResult(_products.TryGetValue(id, out var prod) ? prod : null);

        public Task AddAsync(Product product)
        {
            _products[product.Id] = product;
            return Task.CompletedTask;
        }

        public Task UpdateAsync(Product product)
        {
            _products[product.Id] = product;
            return Task.CompletedTask;
        }

        public Task<bool> DeleteAsync(Guid id)
        {
            return Task.FromResult(_products.TryRemove(id, out _));
        }
    }
}
