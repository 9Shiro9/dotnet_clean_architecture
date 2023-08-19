using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class SaleOrderConfiguration : IEntityTypeConfiguration<SaleOrder>
    {
        public void Configure(EntityTypeBuilder<SaleOrder> builder)
        {
            builder.HasKey(x => x.SaleOrderId);
            builder.Property(p => p.Subtotal).HasPrecision(18, 2);
            builder.HasOne<Customer>(x => x.Customer).WithMany(x => x.SaleOrders).HasForeignKey(x => x.CustomerId).OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(x => x.SaleOrderItems).WithOne(x => x.SaleOrder).HasForeignKey(x => x.SaleOrderId).OnDelete(DeleteBehavior.NoAction);

        }
    }
}
