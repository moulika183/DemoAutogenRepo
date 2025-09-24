Certainly! Based on the Jira story "Product Catalog API," here's a C# Web API controller for a basic Product Catalog. This sample includes a Product model, a simple in-memory repository for demonstration, and RESTful endpoints.

**Product Model**
```csharp
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }        
    public string Description { get; set; }
    public decimal Price { get; set; }
    public bool IsAvailable { get; set; }
}
```

**In-Memory Product Repository:**  
```csharp
public interface IProductRepository
{
    IEnumerable<Product> GetAll();
    Product Get(int id);
    void Add(Product product);
    void Update(Product product);
    void Delete(int id);
}

public class InMemoryProductRepository : IProductRepository
{
    private readonly List<Product> _products = new()
    {
        new Product { Id = 1, Name = "Laptop", Description = "Gaming laptop", Price = 1299.99m, IsAvailable = true },
        new Product { Id = 2, Name = "Headphones", Description = "Noise cancelling", Price = 199.99m, IsAvailable = true }
    };

    public IEnumerable<Product> GetAll() => _products;

    public Product Get(int id) => _products.FirstOrDefault(p => p.Id == id);

    public void Add(Product product)
    {
        product.Id = _products.Max(p => p.Id) + 1;
        _products.Add(product);
    }

    public void Update(Product product)
    {
        var existing = Get(product.Id);
        if (existing == null) return;
        existing.Name = product.Name;
        existing.Description = product.Description;
        existing.Price = product.Price;
        existing.IsAvailable = product.IsAvailable;
    }

    public void Delete(int id)
    {
        var product = Get(id);
        if (product != null)
            _products.Remove(product);
    }
}
```

**Product Catalog Controller:**
```csharp
[Route("api/[controller]")]
[ApiController]
public class ProductCatalogController : ControllerBase
{
    private readonly IProductRepository _repository;

    public ProductCatalogController()
    {
        // In production, register via DI
        _repository = new InMemoryProductRepository();
    }

    [HttpGet]
    public ActionResult<IEnumerable<Product>> GetProducts()
    {
        return Ok(_repository.GetAll());
    }

    [HttpGet("{id}")]
    public ActionResult<Product> GetProduct(int id)
    {
        var product = _repository.Get(id);
        if (product == null) return NotFound();
        return Ok(product);
    }

    [HttpPost]
    public ActionResult<Product> CreateProduct([FromBody] Product product)
    {
        _repository.Add(product);
        return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateProduct(int id, [FromBody] Product product)
    {
        if (id != product.Id) return BadRequest();
        if (_repository.Get(id) == null) return NotFound();

        _repository.Update(product);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteProduct(int id)
    {
        var existing = _repository.Get(id);
        if (existing == null) return NotFound();

        _repository.Delete(id);
        return NoContent();
    }
}
```

**Usage:**  
- GET /api/ProductCatalog: List all products  
- GET /api/ProductCatalog/1: Get product with id=1  
- POST /api/ProductCatalog: Add new product  
- PUT /api/ProductCatalog/1: Update product with id=1  
- DELETE /api/ProductCatalog/1: Delete product with id=1  

Let me know if you want Entity Framework integration, Dtos, or further customization!