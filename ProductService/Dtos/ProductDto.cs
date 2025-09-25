```csharp
namespace ProductService.Dtos;

public class ProductDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public decimal Price { get; set; }
}
```
