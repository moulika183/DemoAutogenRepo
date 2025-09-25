using Microsoft.OpenApi.Models;
using ProductCatalogManagement.Middleware;
using ProductCatalogManagement.Repositories;
using ProductCatalogManagement.Services;

var builder = WebApplication.CreateBuilder(args);

// Logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Product Catalog API", Version = "v1" });
});
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

// Dependency injection
builder.Services.AddSingleton<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();

var app = builder.Build();

// Global error handling
app.UseGlobalErrorHandling();

// Swagger
app.UseSwagger();
app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v1/swagger.json", "Product Catalog API v1"));

// CORS
app.UseCors();

// Routing
app.UseAuthorization();
app.MapControllers();

app.Run();
