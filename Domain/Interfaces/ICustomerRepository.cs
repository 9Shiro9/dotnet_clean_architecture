namespace Domain.Interfaces
{
    public interface ICustomerRepository :  IBaseRepository<Customer>
    {
        Task<IEnumerable<Customer>> GetCustomersByNameAsync(string customerName);
    }
}
