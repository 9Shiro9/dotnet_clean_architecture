namespace Domain.Interfaces
{
    public interface ISupplierRepository :  IBaseRepository<Supplier>
    {
        Task<IEnumerable<Supplier>> GetSuppliersByNameAsync(string supplierName);
    }
}
