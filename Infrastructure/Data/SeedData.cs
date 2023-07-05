using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public static class SeedData
    {
        public static void Seed(ModelBuilder builder)
        {
            //Suppliers Seed
            var supplier_1 = new Supplier() { Id = Guid.NewGuid().ToString(), Name = "John Smith", Address = "Yangon" };
            var supplier_2 = new Supplier() { Id = Guid.NewGuid().ToString(), Name = "David", Address = "Yangon" };

            builder.Entity<Supplier>().HasData(new[] { supplier_1, supplier_2 });


            //Products Seed
            var product_1 = new Product() { Id = Guid.NewGuid().ToString(), SupplierId = supplier_1.Id, Name = "IPhone 11", Description = "IPhone 11", Price = 1000, Quantity = 50 };
            var product_2 = new Product() { Id = Guid.NewGuid().ToString(), SupplierId = supplier_2.Id, Name = "IPhone 12", Description = "IPhone 12", Price = 1500, Quantity = 100 };

            builder.Entity<Product>().HasData(new[] { product_1, product_2 });

            //Orders Seed
            var order_1 = new PurchaseOrder { Id = Guid.NewGuid().ToString(), OrderNumber = "0001", OrderDate = DateTime.Now };
            var order_items_1 = new[] { new PurchaseOrderItem{ Id= Guid.NewGuid().ToString(), PurchaseOrderId = order_1.Id, ProductId = product_1.Id, UnitPrice = product_1.Price, Quantity = 2, TotalPrice = 2 * product_1.Price },
                new PurchaseOrderItem{ Id= Guid.NewGuid().ToString(), PurchaseOrderId = order_1.Id, ProductId = product_2.Id, UnitPrice = product_2.Price, Quantity = 2, TotalPrice = 2 * product_2.Price } };
           

            builder.Entity<PurchaseOrder>().HasData(order_1);
            builder.Entity<PurchaseOrderItem>().HasData(order_items_1);
        }
    }
}
