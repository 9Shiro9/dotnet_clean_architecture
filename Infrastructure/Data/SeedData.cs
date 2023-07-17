using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public static class SeedData
    {
        public static void Seed(ModelBuilder builder)
        {
            ////Customers Seed
            //var customer = new Customer() { Id = Guid.NewGuid().ToString(), Name = "John Smith", Address = "Yangon" };

            //builder.Entity<Customer>().HasData(new[] { customer });

        }
    }
}
