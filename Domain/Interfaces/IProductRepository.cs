namespace Domain.Interfaces
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        Task<IEnumerable<Product>> GetProductsBySupplierIdAsync(string supplierId);
    }
}
