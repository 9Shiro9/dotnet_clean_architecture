using Application.Interfaces;
using Application.ViewModels.Product;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        public async Task<ActionResult<IEnumerable<ProductViewModel>>> GetProducts()
        {
            var result = await _productService.GetProductsAsync(); 

            if(result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductViewModel>> GetProductById(string id)
        {
            var result = await _productService.GetProductByIdAsync(id);

            if(result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet("supplier/{supplierId}")]
        public async Task<ActionResult<IEnumerable<ProductViewModel>>> GetProductsBySupplierId(string supplierId)
        {
            var result = await _productService.GetProductsBySupplierIdAsync(supplierId);

            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(ProductCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var product = new Product()
                {
                    Name = model.Name,
                    Description = model.Description,
                    Price = model.Price,
                    Quantity = model.Quantity,
                    SupplierId = model.SupplierId
                };

                var result = await _productService.AddProductAsync(product);

                if (result)
                {
                    return Ok(result);
                }
            }
            return BadRequest();
        }
    }
}
