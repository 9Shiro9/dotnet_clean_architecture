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

        public async Task<bool> AddPurchaseOrder(PurchaseOrder purchaseOrder, IEnumerable<PurchaseOrderItem> purchaseOrderItems)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                await _purchaseOrderRepository.AddAsync(purchaseOrder);
                await _purchaseOrderItemRepository.AddRangeAsync(purchaseOrderItems);

                await _unitOfWork.CommitTransactionAsync();
                await _unitOfWork.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{ExceptionType}: {Message}", ex.GetType().Name, ex.Message);

                await _unitOfWork.RollbackTransactionAsync();
                return false;
            }
        }

        public async Task<PurchaseOrder> GetPurchaseOrderByIdAsync(string purchaseOrderId)
        {
            return await _purchaseOrderRepository.GetByIdAsync(purchaseOrderId);
        }

        public async Task<IEnumerable<PurchaseOrder>> GetPurchaseOrdersAsync()
        {
            return await _purchaseOrderRepository.GetListAsync();
        }
    }
}
