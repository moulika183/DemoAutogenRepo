Certainly! Below is a sample implementation for a Product Catalog API in C# using ASP.NET Core (.NET 8+ assumed). This is a simple RESTful API with endpoints to manage products.

**Entities/Product.cs**
```csharp
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal Price { get; set; }
    public string Category { get; set; } = null!;
}
```

**Data/IProductRepository.cs**
```csharp
public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllAsync();
    Task<Product?> GetByIdAsync(int id);
    Task<Product> AddAsync(Product product);
    Task<Product?> UpdateAsync(Product product);
    Task<bool> DeleteAsync(int id);
}
```

**Data/InMemoryProductRepository.cs**
```csharp
public class InMemoryProductRepository : IProductRepository
{
    private readonly List<Product> _products = new();
    private int _idCounter = 1;

    public Task<IEnumerable<Product>> GetAllAsync()
        => Task.FromResult(_products.AsEnumerable());

    public Task<Product?> GetByIdAsync(int id)
        => Task.FromResult(_products.FirstOrDefault(x => x.Id == id));

    public Task<Product> AddAsync(Product product)
    {
        product.Id = _idCounter++;
        _products.Add(product);
        return Task.FromResult(product);
    }

    public Task<Product?> UpdateAsync(Product product)
    {
        var existing = _products.FirstOrDefault(x => x.Id == product.Id);
        if (existing == null)
            return Task.FromResult<Product?>(null);

        existing.Name = product.Name;
        existing.Description = product.Description;
        existing.Price = product.Price;
        existing.Category = product.Category;

        return Task.FromResult(existing);
    }

    public Task<bool> DeleteAsync(int id)
    {
        var product = _products.FirstOrDefault(x => x.Id == id);
        if (product == null) return Task.FromResult(false);
        _products.Remove(product);
        return Task.FromResult(true);
    }
}
```

**Controllers/ProductsController.cs**
```csharp
[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductRepository _repository;
    public ProductsController(IProductRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<IActionResult> Get() =>
        Ok(await _repository.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var product = await _repository.GetByIdAsync(id);
        return product == null ? NotFound() : Ok(product);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Product product)
    {
        var created = await _repository.AddAsync(product);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Product product)
    {
        if (id != product.Id) return BadRequest();

        var updated = await _repository.UpdateAsync(product);
        return updated == null ? NotFound() : Ok(updated);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _repository.DeleteAsync(id);
        return result ? NoContent() : NotFound();
    }
}
```

**Program.cs** (for registration)
```csharp
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IProductRepository, InMemoryProductRepository>();
builder.Services.AddControllers();

var app = builder.Build();
app.MapControllers();
app.Run();
```

---

**Summary:**  
- Simple in-memory repository for demo purposes.
- Endpoints: `GET`, `POST`, `PUT`, `DELETE` for Products.
- Easy to adjust and extend.
- You can swap `InMemoryProductRepository` for a persistence-based implementation.

Let me know if you need DTOs, validation, pagination, or integration with a real database!