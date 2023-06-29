using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace Application.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly ISupplierRepository _supplierRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<SupplierService> _logger;

        public SupplierService(ISupplierRepository supplierRepository, IUnitOfWork unitOfWork, ILogger<SupplierService> logger)
        {
            _supplierRepository = supplierRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<bool> AddSupplierAsync(Supplier supplier)
        {
            try
            {
                await _supplierRepository.AddAsync(supplier);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{ExceptionType}: {Message}", ex.GetType().Name, ex.Message);
                return false;
            }
        }

        public async Task<bool> AddSuppliersAsync(IEnumerable<Supplier> suppliers)
        {
            try
            {
                await _supplierRepository.AddRangeAsync(suppliers);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{ExceptionType}: {Message}", ex.GetType().Name, ex.Message);

                return false;
            }
        }

        public async Task<Supplier> GetSupplierByIdAsync(string supplierId)
        {
            return await _supplierRepository.GetByIdAsync(supplierId);
        }

        public async Task<IEnumerable<Supplier>> GetSuppliersAsync()
        {
            return await _supplierRepository.GetListAsync();
        }

        public async Task<IEnumerable<Supplier>> SearchSuppliersByNameAsync(string supplierName)
        {
            return await _supplierRepository.GetSuppliersByNameAsync(supplierName);
        }
    }
}
