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
            var order_1 = new PurchaseOrder("0001");
            var order_items_1 = new List<PurchaseOrderItem>();
            order_items_1.Add(new PurchaseOrderItem(order_1.Id, product_1.Id, 1, product_1.Price, 1 * product_1.Price));
            order_items_1.Add(new PurchaseOrderItem(order_1.Id, product_2.Id, 2, product_2.Price, 2 * product_2.Price));

            order_1.TotalPrice = order_items_1.Sum(x => x.TotalPrice);
            order_1.TotalQuantity = order_items_1.Sum(x => x.Quantity);

            builder.Entity<PurchaseOrder>().HasData(order_1);
            builder.Entity<PurchaseOrderItem>().HasData(order_items_1);
        }
    }
}
