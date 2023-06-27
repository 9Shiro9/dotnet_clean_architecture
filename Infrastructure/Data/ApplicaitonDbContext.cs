using Domain.Entities;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Infrastructure
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }        
        public DbSet<Variant> Variants { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            SeedData(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            string category_1_id = Guid.NewGuid().ToString();
            string category_2_id = Guid.NewGuid().ToString();

            string product_1_id = Guid.NewGuid().ToString();
            string product_2_id = Guid.NewGuid().ToString();

            // Seed data for Categories
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = category_1_id, Name = "Electronics" },
                new Category { Id = category_2_id, Name = "Clothing" }
            );

            // Seed data for Products
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = product_1_id,
                    Name = "Smartphone",
                    Description = "High-end smartphone with advanced features",
                    Price = 999.99m
                },
                new Product
                {
                    Id = product_2_id,
                    Name = "T-Shirt",
                    Description = "Comfortable cotton t-shirt",
                    Price = 19.99m
                }
            );

            // Seed data for Variants
            modelBuilder.Entity<Variant>().HasData(
                new Variant { Id = Guid.NewGuid().ToString(), Name = "Black", Price = 999.99m, Stock = 10, ProductId = product_1_id },
                new Variant { Id = Guid.NewGuid().ToString(), Name = "White", Price = 999.99m, Stock = 5, ProductId = product_1_id },
                new Variant { Id = Guid.NewGuid().ToString(), Name = "Blue", Price = 19.99m, Stock = 20, ProductId = product_2_id }
            );

        }
    }
}
