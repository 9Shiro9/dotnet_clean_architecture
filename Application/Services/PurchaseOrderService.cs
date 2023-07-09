using Application.DTOs.PurchaseOrder;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace Application.Services
{
    public class PurchaseOrderService : IPurchaseOrderService
    {
        private readonly IPurchaseOrderRepository _purchaseOrderRepository;
        private readonly IPurchaseOrderItemRepository _purchaseOrderItemRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<PurchaseOrderService> _logger;

        public PurchaseOrderService(IPurchaseOrderRepository purchaseOrderRepository, IPurchaseOrderItemRepository purchaseOrderItemRepository, IUnitOfWork unitOfWork, ILogger<PurchaseOrderService> logger)
        {
            _purchaseOrderRepository = purchaseOrderRepository;
            _purchaseOrderItemRepository = purchaseOrderItemRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<string> CreatePurchaseOrder(CreatePurchaseOrderDto createOrder)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                var order = new PurchaseOrder(createOrder.OrderNumber,createOrder.TotalPrice,createOrder.TotalQuantity);

                var items=new List<PurchaseOrderItem>();

                foreach (var item in createOrder.Items)
                {
                    items.Add(new PurchaseOrderItem(order.Id,item.ProductId,item.Quantity,item.UnitPrice,item.TotalPrice));
                }

                order.TotalQuantity = items.Sum(x => x.Quantity);
                order.TotalPrice = items.Sum(x => x.TotalPrice);

                await _purchaseOrderRepository.AddAsync(order);
                await _purchaseOrderItemRepository.AddRangeAsync(items);

                await _unitOfWork.CommitTransactionAsync();
                await _unitOfWork.SaveChangesAsync();

                return order.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{ExceptionType}: {Message}", ex.GetType().Name, ex.Message);

                await _unitOfWork.RollbackTransactionAsync();
                return string.Empty;
            }
        }

        public async Task<PurchaseOrderDto> GetPurchaseOrderByIdAsync(string purchaseOrderId)
        {
            var order = await _purchaseOrderRepository.GetAsync(x => x.Id == purchaseOrderId, "PurchaseOrderItems,PurchaseOrderItems.Product");

            var orderDto = new PurchaseOrderDto(order.OrderNumber,order.OrderDate,order.TotalPrice,order.TotalQuantity);

            foreach (var item in order.PurchaseOrderItems)
            {
                orderDto.Items.Add(new PurchaseOrderItemDto(item.ProductId,item.Product.Name,item.Quantity,item.UnitPrice,item.TotalPrice));
            }

            return orderDto;
        }

        public async Task<IEnumerable<PurchaseOrderDto>> GetPurchaseOrdersAsync()
        {
            var ordersDto = new List<PurchaseOrderDto>();

            var orders = await _purchaseOrderRepository.GetListAsync("PurchaseOrderItems,PurchaseOrderItems.Product");

            if(orders == null)
            {
                return Enumerable.Empty<PurchaseOrderDto>();
            }

            foreach (var order in orders)
            {
                var orderDto = new PurchaseOrderDto(order.OrderNumber, order.OrderDate, order.TotalPrice, order.TotalQuantity);

                foreach (var item in order.PurchaseOrderItems)
                {
                    orderDto.Items.Add(new PurchaseOrderItemDto(item.ProductId, item.Product.Name, item.Quantity, item.UnitPrice, item.TotalPrice));
                }

                ordersDto.Add(orderDto);
            }

            return ordersDto;
        }
    }
}
