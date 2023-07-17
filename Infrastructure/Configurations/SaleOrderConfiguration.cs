using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class SaleOrderConfiguration : IEntityTypeConfiguration<SaleOrder>
    {
        public void Configure(EntityTypeBuilder<SaleOrder> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(p => p.TotalPrice).HasPrecision(18, 2);

            builder.HasOne(x => x.Customer).WithMany(x => x.SaleOrders).HasForeignKey(x => x.CustomerId);
            builder.HasMany(x => x.SaleOrderItems).WithOne(x => x.SaleOrder).HasForeignKey(x => x.OrderId).OnDelete(DeleteBehavior.Cascade);

        }
    }
}
