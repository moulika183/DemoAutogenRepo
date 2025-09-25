```csharp
using System.ComponentModel.DataAnnotations;

namespace ProductService.Dtos;

public class ProductUpdateDto
{
    [Required]
    public string Name { get; set; } = default!;

    public string? Description { get; set; }

    [Range(0, double.MaxValue)]
    public decimal Price { get; set; }
}
```
