Below is a complete, clean ASP.NET Core Web API solution named `ProductCatalogApi` with all required files, conventions, and separation of concerns.

// File: ProductCatalogApi.sln
```
Microsoft Visual Studio Solution File, Format Version 12.00
# Visual Studio Version 17
VisualStudioVersion = 17.0.31903.59
MinimumVisualStudioVersion = 10.0.40219.1
Project("{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}") = "ProductCatalogApi", "ProductCatalogApi\ProductCatalogApi.csproj", "{7A235F93-F673-42B3-B0ED-2321C404EEC1}"
EndProject
Global
    GlobalSection(SolutionConfigurationPlatforms) = preSolution
        Debug|Any CPU = Debug|Any CPU
        Release|Any CPU = Release|Any CPU
    EndGlobalSection
    GlobalSection(ProjectConfigurationPlatforms) = postSolution
        {7A235F93-F673-42B3-B0ED-2321C404EEC1}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
        {7A235F93-F673-42B3-B0ED-2321C404EEC1}.Debug|Any CPU.Build.0 = Debug|Any CPU
        {7A235F93-F673-42B3-B0ED-2321C404EEC1}.Release|Any CPU.ActiveCfg = Release|Any CPU
        {7A235F93-F673-42B3-B0ED-2321C404EEC1}.Release|Any CPU.Build.0 = Release|Any CPU
    EndGlobalSection
EndGlobal
```

// File: ProductCatalogApi/ProductCatalogApi.csproj
```xml
<Project Sdk="Microsoft.NET.Sdk.Web">

  <PropertyGroup>
    <TargetFramework>net8.0</TargetFramework>
    <Nullable>enable</Nullable>
    <ImplicitUsings>enable</ImplicitUsings>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="Swashbuckle.AspNetCore" Version="6.5.0" />
  </ItemGroup>

</Project>
```

// File: ProductCatalogApi/appsettings.json
```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```

// File: ProductCatalogApi/Program.cs
```csharp
using Microsoft.OpenApi.Models;
using ProductCatalogApi.Middleware;
using ProductCatalogApi.Repositories;
using ProductCatalogApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Product Catalog API",
        Version = "v1",
        Description = "API for managing product catalog"
    });
});

// Dependency Injection
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();

// CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseMiddleware<ErrorHandlingMiddleware>();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
```

// File: ProductCatalogApi/Models/Product.cs
```csharp
namespace ProductCatalogApi.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
    }
}
```

// File: ProductCatalogApi/Dtos/ProductDto.cs
```csharp
using System.ComponentModel.DataAnnotations;

namespace ProductCatalogApi.Dtos
{
    public class ProductDto
    {
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public required string Name { get; set; }
        
        [StringLength(500)]
        public string? Description { get; set; }
        
        [Required]
        [Range(0.01, 100_000)]
        public decimal Price { get; set; }
    }
}
```

// File: ProductCatalogApi/Dtos/ProductResponseDto.cs
```csharp
namespace ProductCatalogApi.Dtos
{
    public class ProductResponseDto
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
    }
}
```

// File: ProductCatalogApi/Controllers/ProductsController.cs
```csharp
using Microsoft.AspNetCore.Mvc;
using ProductCatalogApi.Dtos;
using ProductCatalogApi.Services;

namespace ProductCatalogApi.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(IProductService productService, ILogger<ProductsController> logger)
        {
            _productService = productService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductResponseDto>>> GetAll()
        {
            _logger.LogInformation("Retrieving all products.");
            var products = await _productService.GetAllAsync();
            return Ok(products);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ProductResponseDto>> GetById(Guid id)
        {
            _logger.LogInformation("Retrieving product with id {ProductId}", id);
            var product = await _productService.GetByIdAsync(id);
            if (product == null)
                return NotFound(new { message = $"Product with id {id} not found." });
            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<ProductResponseDto>> Create([FromBody] ProductDto dto)
        {
            _logger.LogInformation("Creating new product: {ProductName}", dto.Name);
            var created = await _productService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<ProductResponseDto>> Update(Guid id, [FromBody] ProductDto dto)
        {
            _logger.LogInformation("Updating product {ProductId}", id);
            var updated = await _productService.UpdateAsync(id, dto);
            if (updated == null)
                return NotFound(new { message = $"Product with id {id} not found." });
            return Ok(updated);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            _logger.LogInformation("Deleting product {ProductId}", id);
            var deleted = await _productService.DeleteAsync(id);
            if (!deleted)
                return NotFound(new { message = $"Product with id {id} not found." });
            return NoContent();
        }
    }
}
```

// File: ProductCatalogApi/Services/IProductService.cs
```csharp
using ProductCatalogApi.Dtos;

namespace ProductCatalogApi.Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductResponseDto>> GetAllAsync();
        Task<ProductResponseDto?> GetByIdAsync(Guid id);
        Task<ProductResponseDto> CreateAsync(ProductDto dto);
        Task<ProductResponseDto?> UpdateAsync(Guid id, ProductDto dto);
        Task<bool> DeleteAsync(Guid id);
    }
}
```

// File: ProductCatalogApi/Services/ProductService.cs
```csharp
using ProductCatalogApi.Dtos;
using ProductCatalogApi.Models;
using ProductCatalogApi.Repositories;

namespace ProductCatalogApi.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        private readonly ILogger<ProductService> _logger;

        public ProductService(IProductRepository repository, ILogger<ProductService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<IEnumerable<ProductResponseDto>> GetAllAsync()
        {
            var products = await _repository.GetAllAsync();
            return products.Select(MapToResponseDto);
        }

        public async Task<ProductResponseDto?> GetByIdAsync(Guid id)
        {
            var product = await _repository.GetByIdAsync(id);
            return product == null ? null : MapToResponseDto(product);
        }

        public async Task<ProductResponseDto> CreateAsync(ProductDto dto)
        {
            var product = new Product
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price
            };
            await _repository.CreateAsync(product);
            _logger.LogInformation("Product created with ID {ProductId}", product.Id);
            return MapToResponseDto(product);
        }

        public async Task<ProductResponseDto?> UpdateAsync(Guid id, ProductDto dto)
        {
            var product = await _repository.GetByIdAsync(id);
            if (product == null)
            {
                _logger.LogWarning("Attempted to update non-existing product {ProductId}", id);
                return null;
            }
            product.Name = dto.Name;
            product.Description = dto.Description;
            product.Price = dto.Price;
            await _repository.UpdateAsync(product);
            _logger.LogInformation("Product updated with ID {ProductId}", id);
            return MapToResponseDto(product);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var deleted = await _repository.DeleteAsync(id);
            if (deleted)
                _logger.LogInformation("Product deleted with ID {ProductId}", id);
            else
                _logger.LogWarning("Attempted to delete non-existing product {ProductId}", id);
            return deleted;
        }

        private static ProductResponseDto MapToResponseDto(Product product) =>
            new()
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price
            };
    }
}
```

// File: ProductCatalogApi/Repositories/IProductRepository.cs
```csharp
using ProductCatalogApi.Models;

namespace ProductCatalogApi.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product?> GetByIdAsync(Guid id);
        Task CreateAsync(Product product);
        Task UpdateAsync(Product product);
        Task<bool> DeleteAsync(Guid id);
    }
}
```

// File: ProductCatalogApi/Repositories/ProductRepository.cs
```csharp
using ProductCatalogApi.Models;

namespace ProductCatalogApi.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private static readonly List<Product> _products = new()
        {
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Demo Widget",
                Description = "A demo widget for the catalog",
                Price = 19.99M
            }
        };

        public Task<IEnumerable<Product>> GetAllAsync()
        {
            return Task.FromResult(_products.AsEnumerable());
        }

        public Task<Product?> GetByIdAsync(Guid id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            return Task.FromResult(product);
        }

        public Task CreateAsync(Product product)
        {
            _products.Add(product);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(Product product)
        {
            var idx = _products.FindIndex(p => p.Id == product.Id);
            if (idx >= 0)
                _products[idx] = product;
            return Task.CompletedTask;
        }

        public Task<bool> DeleteAsync(Guid id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product == null)
                return Task.FromResult(false);
            _products.Remove(product);
            return Task.FromResult(true);
        }
    }
}
```

// File: ProductCatalogApi/Middleware/ErrorHandlingMiddleware.cs
```csharp
using System.Net;
using System.Text.Json;

namespace ProductCatalogApi.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred.");
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var problemDetails = new
            {
                status = (int)HttpStatusCode.InternalServerError,
                title = "An error occurred while processing your request.",
                detail = ex.Message
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var json = JsonSerializer.Serialize(problemDetails);
            await context.Response.WriteAsync(json);
        }
    }
}
```

---

**Instructions:**
- Save each file according to its marker.
- Run `dotnet build` and `dotnet run`.
- Access Swagger UI at `/swagger` for API testing.

Let me know if you need further customization!