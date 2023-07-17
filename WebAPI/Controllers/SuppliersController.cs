using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using WebAPI.Common;
using WebAPI.Resources;

namespace WebAPI.Controllers
{
    [Authorize(Roles = "Administrators")]
    [Route("api/[controller]")]
    [ApiController]
    public class SuppliersController : ControllerBase
    {
        private readonly ICustomerService _supplierService;
        private readonly ILogger<SuppliersController> _logger;
        private readonly IStringLocalizer<SharedResource> _stringLocalizer;
        private readonly IHttpContextAccessor _httpContext;

        public SuppliersController(ICustomerService supplierService, ILogger<SuppliersController> logger, IStringLocalizer<SharedResource> stringLocalizer, IHttpContextAccessor httpContext)
        {
            _supplierService = supplierService;
            _logger = logger;
            _stringLocalizer = stringLocalizer;
            _httpContext = httpContext;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<Customer>>>> GetSuppliers()
        {

            var response = new ApiResponse<IEnumerable<Customer>>();

            try
            {
                var suppliers = await _supplierService.GetCustomersAsync();

                response.Code = StatusCodes.Status200OK;

                if (suppliers != null && suppliers.Any())
                {
                    response.Data = suppliers;
                }
                else
                {
                    response.Message = ApiResponseMessage.RecordNotFound.ToString();
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving suppliers.");
                response.Code = StatusCodes.Status500InternalServerError;
            }

            return response;
        }
    }
}
