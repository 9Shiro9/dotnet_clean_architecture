using Application.DTOs.Product;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Common;
using WebAPI.ViewModels.Product;

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
        public async Task<ActionResult<ApiResponse<IEnumerable<ProductDto>>>> GetProducts()
        {
            var response = new ApiResponse<IEnumerable<ProductDto>>();

            var result = await _productService.GetProductsAsync();

            if (result == null)
            {
                _logger.LogInformation("There is no record in products.");

                response.Code = StatusCodes.Status204NoContent;
                response.Status = ApiResponseStatus.Success.ToString();
                response.Message = ApiResponseMessage.RecordNotFound.ToString();

                return response;
            }

            response.Code = StatusCodes.Status200OK;
            response.Data = result;

            return response;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<ProductDto>>> GetProductById(string id)
        {
            var response = new ApiResponse<ProductDto>();

            var result = await _productService.GetProductByIdAsync(id);

            if (result == null)
            {
                response.Code = StatusCodes.Status204NoContent;
                response.Status = ApiResponseStatus.Success.ToString();
                response.Message = ApiResponseMessage.RecordNotFound.ToString();

                return response;
            }

            response.Code = StatusCodes.Status200OK;
            response.Data = result;

            return response;
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<ProductDto>>> AddProduct(CreateProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                var response = new ApiResponse<ProductDto>();

                var createProductDto =new CreateProductDto(model.Code,model.Description,model.BuyingPrice,model.SellingPrice,model.Quantity);

                var id = await _productService.AddProductAsync(createProductDto);

                if (string.IsNullOrEmpty(id))
                {
                    response.Code = StatusCodes.Status204NoContent;
                    response.Status = ApiResponseStatus.Success.ToString();
                    response.Message = ApiResponseMessage.RecordNotFound.ToString();

                    return response;
                }

                var result = await _productService.GetProductByIdAsync(id);

                response.Code = StatusCodes.Status200OK;
                response.Data = result;

                return response;
            }
            return BadRequest();
        }
    }
}
