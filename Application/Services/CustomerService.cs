using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CustomerService> _logger;

        public CustomerService(ICustomerRepository supplierRepository, IUnitOfWork unitOfWork, ILogger<CustomerService> logger)
        {
            _customerRepository = supplierRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<IEnumerable<Customer>> GetCustomersAsync()
        {
            return await _customerRepository.GetListAsync();
        }

        public async Task<IEnumerable<Customer>> GetCustomersByNameAsync(string customerName)
        {
            return await _customerRepository.GetCustomersByNameAsync(customerName);
        }

        public async Task<Customer> GetCustomerByIdAsync(string customerId)
        {
           return await _customerRepository.GetByIdAsync(customerId);
        }

        public async Task<bool> CreateCustomerAsync(Customer customer)
        {
            try
            {
                await _customerRepository.AddAsync(customer);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{ExceptionType}: {Message}", ex.GetType().Name, ex.Message);
                return false;
            }
        }
    }
}
