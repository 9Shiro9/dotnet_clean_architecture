using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class SaleOrderItemConfiguration : IEntityTypeConfiguration<SaleOrderItem>
    {
        public void Configure(EntityTypeBuilder<SaleOrderItem> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(p => p.TotalPrice).HasPrecision(18, 2);
            builder.Property(p => p.UnitPrice).HasPrecision(18, 2);

            builder.HasOne(v => v.Product).WithMany(x => x.SaleOrderItems);
            builder.HasOne(o => o.SaleOrder).WithMany(x => x.SaleOrderItems);
        }
    }
    
}
