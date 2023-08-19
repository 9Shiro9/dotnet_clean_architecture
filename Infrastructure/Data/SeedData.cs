using Domain.Entities;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public static class SeedData
    {
        public static void Seed(ModelBuilder builder)
        {
            //Users Seed
            PasswordHasher<ApplicationUser> passwordHasher = new PasswordHasher<ApplicationUser>();

            var _user1 = new ApplicationUser { Id = Guid.NewGuid(), UserName = "user1@gmail.com", Email = "user1@gmail.com" };
            _user1.PasswordHash = passwordHasher.HashPassword(_user1, "user@123");

            var _user2 = new ApplicationUser { Id = Guid.NewGuid(), UserName = "user2@gmail.com", Email = "user2@gmail.com" };
            _user2.PasswordHash = passwordHasher.HashPassword(_user2, "user@123");

            builder.Entity<ApplicationUser>().HasData(_user1);
            builder.Entity<ApplicationUser>().HasData(_user2);

            //Customers Seed
            var customers = new List<Customer>()
            {
                new Customer(){ CustomerId = Guid.NewGuid().ToString(), Name = "User1", EmailAddress = "user1@gmail.com", PhoneNumber = "123456789" , UserId = _user1.Id.ToString() },
                new Customer(){ CustomerId = Guid.NewGuid().ToString(), Name = "User2", EmailAddress = "user2@gmail.com", PhoneNumber = "123456789" , UserId = _user2.Id.ToString() }
            };

            builder.Entity<Customer>().HasData(customers);

            //Category Seed

            var _mobileCategory = new Category() { CategoryId = Guid.NewGuid().ToString(), Code = "Mobile", Descripton = "Mobile" };
            var _computerCategory = new Category() { CategoryId = Guid.NewGuid().ToString(), Code = "Computer", Descripton = "Computer" };


            builder.Entity<Category>().HasData(new List<Category>() { _mobileCategory, _computerCategory});

            //Product Seed
            var dell = new Product()
            {
                ProductId = Guid.NewGuid().ToString(),
                Code = "Dell",
                Description = "Dell",
                CategoryId = _computerCategory.CategoryId,
                Quantity = 100,
                SellingPrice = 100,
                BuyingPrice = 90
            };

            var iphone = new Product()
            {
                ProductId = Guid.NewGuid().ToString(),
                Code = "IPhone",
                Description = "IPhone",
                CategoryId = _mobileCategory.CategoryId,
                Quantity = 50,
                SellingPrice = 500,
                BuyingPrice = 400
            };

            builder.Entity<Product>().HasData(new List<Product>() { dell, iphone });

        }
    }
}
