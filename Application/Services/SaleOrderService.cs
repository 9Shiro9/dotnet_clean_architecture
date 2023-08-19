using Application.DTOs.SaleOrder;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace Application.Services
{
    public class SaleOrderService : ISaleOrderService
    {
        private readonly ISaleOrderRepository _saleOrderRepository;
        private readonly ISaleOrderItemRepository _saleOrderItemRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<SaleOrderService> _logger;

        public SaleOrderService(ISaleOrderRepository purchaseOrderRepository, ISaleOrderItemRepository purchaseOrderItemRepository, IUnitOfWork unitOfWork, ILogger<SaleOrderService> logger)
        {
            _saleOrderRepository = purchaseOrderRepository;
            _saleOrderItemRepository = purchaseOrderItemRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<string> CreateSaleOrder(CreateSaleOrderDto createOrder)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                var order = new SaleOrder(createOrder.OrderNumber,createOrder.OrderDate,createOrder.CustomerId);

                var items = new List<SaleOrderItem>();

                foreach (var item in createOrder.Items)
                {
                    items.Add(new SaleOrderItem(order.SaleOrderId,item.ProductId,item.Quantity,item.UnitPrice));
                }

                order.Quantity = items.Sum(x => x.Quantity);
                order.Subtotal = items.Sum(x => x.Total);

                await _saleOrderRepository.AddAsync(order);
                await _saleOrderItemRepository.AddRangeAsync(items);

                await _unitOfWork.CommitTransactionAsync();
                await _unitOfWork.SaveChangesAsync();

                return order.SaleOrderId;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{ExceptionType}: {Message}", ex.GetType().Name, ex.Message);

                await _unitOfWork.RollbackTransactionAsync();
                return string.Empty;
            }
        }

        public async Task<SaleOrderDto> GetSaleOrderByIdAsync(string orderId)
        {
            var order = await _saleOrderRepository.GetAsync(x => x.SaleOrderId == orderId, "SaleOrderItems,Customer,SaleOrderItems.Product");

            var orderDto = new SaleOrderDto(order.OrderNumber, order.OrderDate, order.Subtotal, order.Quantity, order.CustomerId, order.Customer.Name);

            foreach (var item in order.SaleOrderItems)
            {
                orderDto.Items.Add(new SaleOrderItemDto(item.ProductId, item.Product.Code, item.Quantity, item.UnitPrice, item.Total));
            }

            return orderDto;
        }

        public async Task<IEnumerable<SaleOrderDto>> GetSaleOrdersAsync()
        {
            var ordersDto = new List<SaleOrderDto>();

            var orders = await _saleOrderRepository.GetListAsync("SaleOrderItems,Customer,SaleOrderItems.Product");

            if (orders == null)
            {
                return Enumerable.Empty<SaleOrderDto>();
            }

            foreach (var order in orders)
            {
                var orderDto = new SaleOrderDto(order.OrderNumber, order.OrderDate, order.Subtotal, order.Quantity, order.CustomerId, order.Customer.Name);

                foreach (var item in order.SaleOrderItems)
                {
                    orderDto.Items.Add(new SaleOrderItemDto(item.ProductId, item.Product.Code, item.Quantity, item.UnitPrice, item.Total));
                }

                ordersDto.Add(orderDto);
            }

            return ordersDto;
        }

        public async Task<IEnumerable<SaleOrderDto>> GetSaleOrdersByCustomerIdAsync(string customerId)
        {
            var ordersDto = new List<SaleOrderDto>();

            var orders = await _saleOrderRepository.GetSaleOrdersByCustomerIdAsync(customerId);

            foreach (var order in orders)
            {
                var orderDto = new SaleOrderDto(order.OrderNumber,order.OrderDate,order.Subtotal,order.Quantity,order.CustomerId,order.Customer.Name);
      
                foreach (var item in order.SaleOrderItems)
                {
                    orderDto.Items.Add(new SaleOrderItemDto(item.ProductId, item.Product.Code, item.Quantity, item.UnitPrice, item.Total));
                }

                ordersDto.Add(orderDto);
            }

            return ordersDto;

        }
    }
}
