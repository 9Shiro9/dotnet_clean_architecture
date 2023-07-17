using Application.DTOs.SaleOrder;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Common;
using WebAPI.ViewModels.SaleOrder;

namespace WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SaleOrdersController : ControllerBase
    {
        private readonly ISaleOrderService _saleOrderService;
        private readonly ILogger<SaleOrdersController> _logger;

        public SaleOrdersController(ISaleOrderService saleOrderService, ILogger<SaleOrdersController> logger)
        {
            _saleOrderService = saleOrderService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<SaleOrderDto>>>> GetSaleOrdersAsync()
        {
            var response = new ApiResponse<IEnumerable<SaleOrderDto>>();

            var result = await _saleOrderService.GetSaleOrdersAsync();

            if (result == null)
            {
                response.Code= StatusCodes.Status204NoContent;
                response.Status= ApiResponseStatus.Success.ToString();
                response.Message= ApiResponseMessage.RecordNotFound.ToString();

                return response;
            }

            response.Code= StatusCodes.Status200OK;
            response.Data= result;

            return response;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<SaleOrderDto>>> GetSaleOrderByIdAsync(string id)
        {
            var response = new ApiResponse<SaleOrderDto>();

            if(string.IsNullOrEmpty(id))
            {
                response.Code = StatusCodes.Status400BadRequest;
                return response;
            }

            var result = await _saleOrderService.GetSaleOrderByIdAsync(id);

            if(result == null)
            {
                response.Code= StatusCodes.Status500InternalServerError;
                return response;
            }

            response.Code=StatusCodes.Status200OK;
            response.Data = result;

            return response;
        }


        [HttpPost]
        public async Task<ActionResult<ApiResponse<SaleOrderDto>>> CreatePurchaseOrder(CreateSaleOrderViewModel model)
        {
            var response = new ApiResponse<SaleOrderDto>();

            if (!ModelState.IsValid || model.Items == null || model.Items.Count == 0)
            {
                response.Code = StatusCodes.Status400BadRequest;

                return response;
            }
           
            var orderItemsDto = new List<CreateSaleOrderItemDto>();

            foreach (var item in model.Items)
            {
                orderItemsDto.Add(new CreateSaleOrderItemDto(item.ProductId,item.Quantity,item.UnitPrice,item.TotalPrice));
            }

            var orderDto = new CreateSaleOrderDto(model.OrderNumber,model.CustomerId, orderItemsDto);

            var orderId = await _saleOrderService.CreateSaleOrder(orderDto);

            if(!string.IsNullOrEmpty(orderId))
            {
                //get order by id
                var order = await _saleOrderService.GetSaleOrderByIdAsync(orderId);

                if(order != null)
                {
                    response.Code = StatusCodes.Status200OK;
                    response.Data = order;
                }
            }
            else
            {
                response.Code = StatusCodes.Status500InternalServerError;
            }
       
            return response;
        }
    }
}
