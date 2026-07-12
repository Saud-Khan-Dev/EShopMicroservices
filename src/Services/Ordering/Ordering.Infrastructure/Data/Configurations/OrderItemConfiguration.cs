using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
  public void Configure(EntityTypeBuilder<OrderItem> builder)
  {
    builder.HasKey(x => x.Id);
    builder.Property(x => x.Id)
    .HasConversion(orderItemId => orderItemId.Value, dbId => OrderItemId.Of(dbId));

    builder.HasOne<Product>()
    .WithMany()
    .HasForeignKey(f => f.ProductId);

    builder.Property(x => x.Quantity).IsRequired();
    builder.Property(x => x.Price).IsRequired();

  }
}