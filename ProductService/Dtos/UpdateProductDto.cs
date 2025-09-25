```csharp
namespace ProductService.Dtos;

public class UpdateProductDto
{
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public string SKU { get; set; } = default!;
}
```
