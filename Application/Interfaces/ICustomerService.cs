using Domain.Entities;

namespace Application.Interfaces
{
    public interface ICustomerService
    {
        Task<IEnumerable<Customer>> GetCustomersAsync();        
        Task<IEnumerable<Customer>> GetCustomersByNameAsync(string customerName);
        Task<Customer> GetCustomerByIdAsync(string customerId);
        Task<bool> CreateCustomerAsync(Customer customer);
    }
}
