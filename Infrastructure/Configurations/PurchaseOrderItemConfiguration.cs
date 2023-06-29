using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class PurchaseOrderItemConfiguration : IEntityTypeConfiguration<PurchaseOrderItem>
    {
        public void Configure(EntityTypeBuilder<PurchaseOrderItem> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(p => p.TotalPrice).HasPrecision(18, 2);
            builder.Property(p => p.UnitPrice).HasPrecision(18, 2);

            builder.HasOne(v => v.Product).WithMany(x => x.PurchaseOrderItems);
            builder.HasOne(o => o.PurchaseOrder).WithMany(x => x.PurchaseOrderItems);
        }
    }
    
}
