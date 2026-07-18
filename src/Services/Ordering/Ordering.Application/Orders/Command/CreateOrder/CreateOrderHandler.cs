using BuildingBlocks.CQRS;

public class CreateOrderHandler(IApplicationDbContext dbContext) : ICommandHandler<CreateOrderCommand, CreateOrderResult>
{
  public async Task<CreateOrderResult> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
  {
    var order = CreateNewOrder(command.Order);
    dbContext.Orders.Add(order);
    await dbContext.SaveChangesAsync(cancellationToken);

    return new CreateOrderResult(order.Id.Value);
  }

  private Order CreateNewOrder(OrderDto order)
  {
    var shippingAddress = Address.Of(order.ShippingAddress.FirstName, order.ShippingAddress.LastName, order.ShippingAddress.EmailAddress, order.ShippingAddress.AddressLine, order.ShippingAddress.Country, order.ShippingAddress.State, order.ShippingAddress.ZipCode);
    var billingAddres = Address.Of(order.BillingAddress.FirstName, order.BillingAddress.LastName, order.BillingAddress.EmailAddress, order.BillingAddress.AddressLine, order.BillingAddress.Country, order.BillingAddress.State, order.BillingAddress.ZipCode);

    var newOrder = Order.Create(id: OrderId.Of(Guid.NewGuid()), customerId: CustomerId.Of(order.CustomerId), orderName: OrderName.Of(order.OrderName), shippingAddress: shippingAddress, billingAddress: billingAddres, payment: Payment.Of(order.PaymentDto.CardName, order.PaymentDto.CardNumber, order.PaymentDto.Expiration, order.PaymentDto.Cvv, order.PaymentDto.PaymentMethod));

    foreach (var OrderItemDto in order.OrderItems)
    {
      newOrder.Add(ProductId.Of(OrderItemDto.ProductId), OrderItemDto.Quantity, OrderItemDto.Price);
    }

    return newOrder;
  }
}