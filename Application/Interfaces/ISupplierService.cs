using Domain.Entities;

namespace Application.Interfaces
{
    public interface ISupplierService
    {
        Task<IEnumerable<Supplier>> GetSuppliersAsync();        
        Task<IEnumerable<Supplier>> SearchSuppliersByNameAsync(string supplierName);
        Task<Supplier> GetSupplierByIdAsync(string supplierId);
        Task<bool> AddSuppliersAsync(IEnumerable<Supplier> suppliers);
        Task<bool> AddSupplierAsync(Supplier supplier);
    }
}
