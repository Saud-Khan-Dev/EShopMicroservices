using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
  public void Configure(EntityTypeBuilder<Order> builder)
  {
    builder.HasKey(x => x.Id);
    builder.Property(x => x.Id)
    .HasConversion(orderId => orderId.Value, dbId => OrderId.Of(dbId));

    builder.HasOne<Customer>()
    .WithMany()
    .HasForeignKey(f => f.CustomerId).IsRequired();


    builder.HasMany<OrderItem>()
    .WithOne()
    .HasForeignKey(f => f.OrderId).IsRequired();


    builder.ComplexProperty(o => o.OrderName
    , nameBuilder =>
    {
      nameBuilder.Property(n => n.Value)
      .HasColumnName(nameof(Order.OrderName))
      .HasMaxLength(100)
      .IsRequired();
    }
    );

    builder.ComplexProperty(o => o.ShippingAddress, addressBuilder =>
    {
      addressBuilder.Property(a => a.FirstName).HasMaxLength(50).IsRequired();
      addressBuilder.Property(a => a.LastName).HasMaxLength(50).IsRequired();
      addressBuilder.Property(a => a.Email).HasMaxLength(180).IsRequired();
      addressBuilder.Property(a => a.AddressLine).HasMaxLength(50).IsRequired();
      addressBuilder.Property(a => a.Country).HasMaxLength(50).IsRequired();
      addressBuilder.Property(a => a.State).HasMaxLength(50).IsRequired();
      addressBuilder.Property(a => a.ZipCode).HasMaxLength(5).IsRequired();
    });

    builder.ComplexProperty(o => o.BillingAddress, addressBuilder =>
    {
      addressBuilder.Property(a => a.FirstName).HasMaxLength(50).IsRequired();
      addressBuilder.Property(a => a.LastName).HasMaxLength(50).IsRequired();
      addressBuilder.Property(a => a.Email).HasMaxLength(180).IsRequired();
      addressBuilder.Property(a => a.AddressLine).HasMaxLength(50).IsRequired();
      addressBuilder.Property(a => a.Country).HasMaxLength(50).IsRequired();
      addressBuilder.Property(a => a.State).HasMaxLength(50).IsRequired();
      addressBuilder.Property(a => a.ZipCode).HasMaxLength(5).IsRequired();
    });


    builder.ComplexProperty(o => o.Payment, paymentBuilder =>
{
  paymentBuilder.Property(p => p.CardName)
      .HasMaxLength(50)
      .IsRequired();

  paymentBuilder.Property(p => p.CardNumber)
      .HasMaxLength(24)
      .IsRequired();

  paymentBuilder.Property(p => p.Expiration)
      .HasMaxLength(5)
      .IsRequired();

  paymentBuilder.Property(p => p.CVV)
      .HasMaxLength(3)
      .IsRequired();

  paymentBuilder.Property(p => p.PaymentMethod)
      .IsRequired();
});

    builder.Property(x => x.Status)
    .HasDefaultValue(OrderStatus.Draft)
    .HasConversion(s => s.ToString(), dbStatus => (OrderStatus)Enum.Parse(typeof(OrderStatus), dbStatus));

    builder.Property(x => x.TotalPrice);
  }
}