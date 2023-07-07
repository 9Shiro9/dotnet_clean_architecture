using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using WebAPI.Resources;

namespace WebAPI.Controllers
{
    [Authorize(Roles = "Administrators")]
    [Route("api/[controller]")]
    [ApiController]
    public class SuppliersController : ControllerBase
    {
        private readonly ISupplierService _supplierService;
        private readonly ILogger<SuppliersController> _logger;
        private readonly IStringLocalizer<SharedResource> _stringLocalizer;
        private readonly IHttpContextAccessor _httpContext;

        public SuppliersController(ISupplierService supplierService, ILogger<SuppliersController> logger, IStringLocalizer<SharedResource> stringLocalizer, IHttpContextAccessor httpContext)
        {
            _supplierService = supplierService;
            _logger = logger;
            _stringLocalizer = stringLocalizer;
            _httpContext = httpContext;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<IEnumerable<Supplier>>> GetSuppliers()
        {
            var result = await _supplierService.GetSuppliersAsync();

            if (result  == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
    }
}
