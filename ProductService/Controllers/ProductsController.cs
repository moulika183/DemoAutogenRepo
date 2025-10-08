using Microsoft.AspNetCore.Mvc;
using ProductService.Services;
using ProductService.Dtos;

namespace ProductService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(IProductService productService, ILogger<ProductsController> logger)
        {
            _productService = productService;
            _logger = logger;
        }

        /// <summary>
        /// Get all products in catalog.
        /// </summary>
        /// <returns>List of products</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAll()
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(products);
        }

        /// <summary>
        /// Get product by ID.
        /// </summary>
        /// <param name="id">Product ID</param>
        /// <returns>Product matching the ID</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetById(Guid id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                _logger.LogInformation("Product with id {ProductId} not found.", id);
                return NotFound();
            }
            return Ok(product);
        }
    }
}
