using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class CustomerRepository : EfBaseRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Customer>> GetCustomersByNameAsync(string customerName)
        {
            return await _dbContext.Customers.Where(x => x.Name.Contains(customerName)).ToListAsync();
        }
    }
}
