using Application.DTOs.PurchaseOrder;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Common;
using WebAPI.ViewModels.PurchaseOrder;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseOrdersController : ControllerBase
    {
        private readonly IPurchaseOrderService _purchaseOrderService;
        private readonly ILogger<PurchaseOrdersController> _logger;

        public PurchaseOrdersController(IPurchaseOrderService purchaseOrderService, ILogger<PurchaseOrdersController> logger)
        {
            _purchaseOrderService = purchaseOrderService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<PurchaseOrderDto>>>> GetPurchaseOrders()
        {
            var response = new ApiResponse<IEnumerable<PurchaseOrderDto>>();

            var result = await _purchaseOrderService.GetPurchaseOrdersAsync();

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
        public async Task<ActionResult<ApiResponse<PurchaseOrderDto>>> GetPurchaseOrderById(string id)
        {
            var response = new ApiResponse<PurchaseOrderDto>();

            if(string.IsNullOrEmpty(id))
            {
                response.Code = StatusCodes.Status400BadRequest;
                return response;
            }

            var result = await _purchaseOrderService.GetPurchaseOrderByIdAsync(id);

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
        public async Task<ActionResult<ApiResponse<PurchaseOrderDto>>> CreatePurchaseOrder(CreatePurchaseOrderViewModel model)
        {
            var response = new ApiResponse<PurchaseOrderDto>();

            if (!ModelState.IsValid || model.Items == null || model.Items.Count == 0)
            {
                response.Code = StatusCodes.Status400BadRequest;

                return response;
            }
           
            var orderItemsDto = new List<CreatePurchaseOrderItemDto>();

            foreach (var item in model.Items)
            {
                orderItemsDto.Add(new CreatePurchaseOrderItemDto(item.ProductId,item.Quantity,item.UnitPrice,item.TotalPrice));
            }

            var orderDto = new CreatePurchaseOrderDto(model.OrderNumber, orderItemsDto);

            var orderId = await _purchaseOrderService.CreatePurchaseOrder(orderDto);

            if(!string.IsNullOrEmpty(orderId))
            {
                //get order by id
                var order = await _purchaseOrderService.GetPurchaseOrderByIdAsync(orderId);

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
