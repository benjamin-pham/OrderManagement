using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderManagement.Domain.Orders;

namespace OrderManagement.Infrastructure.Configurations;
internal class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
{
    public void Configure(EntityTypeBuilder<OrderDetail> builder)
    {
        builder.ToTable("OrderDetails");

        builder.HasKey(detail => detail.Id);

        builder.Property(detail => detail.ProductName)
            .HasMaxLength(255)
            .IsUnicode()
            .IsRequired();

        builder.Property(detail => detail.Quantity)
            .IsRequired();

        builder.Property(detail => detail.Price)
            .HasPrecision(18, 2)
            .IsRequired();
    }
}
