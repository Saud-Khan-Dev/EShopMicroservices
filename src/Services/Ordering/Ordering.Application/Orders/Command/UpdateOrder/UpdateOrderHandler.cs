using BuildingBlocks.CQRS;

public class UpdateOrderHandler(IApplicationDbContext dbContext) : ICommandHandler<UpdateOrderCommand, UpdateOrderResult>
{
  public async Task<UpdateOrderResult> Handle(UpdateOrderCommand command, CancellationToken cancellationToken)
  {
    var orderId = OrderId.Of(command.Order.Id);
    var order = await dbContext.Orders.FindAsync([orderId], cancellationToken);

    if (order is null)
    {
      throw new OrderNotFoundException(command.Order.Id);
    }

    UpdateOrderWithNewValues(order, command.Order.Id);

    dbContext.Orders.Update(order);
    await dbContext.SaveChangesAsync(cancellationToken);

    return new UpdateOrderResult(true);
  }

  private void UpdateOrderWithNewValues(Order order, Guid id)
  {
    var shippingAddress = Address.Of(order.ShippingAddress.FirstName, order.ShippingAddress.LastName, order.ShippingAddress.Email!, order.ShippingAddress.AddressLine, order.ShippingAddress.Country, order.ShippingAddress.State, order.ShippingAddress.ZipCode);
    var billingAddress = Address.Of(order.BillingAddress.FirstName, order.BillingAddress.LastName, order.BillingAddress.Email!, order.BillingAddress.AddressLine, order.BillingAddress.Country, order.BillingAddress.State, order.BillingAddress.ZipCode);
    var payment = Payment.Of(order.Payment.CardName!, order.Payment.CardNumber, order.Payment.Expiration, order.Payment.CVV, order.Payment.PaymentMethod);
    order.Update(customerId: order.CustomerId, orderName: OrderName.Of(order.OrderName.Value), shippingAddress: shippingAddress, billingAddress: billingAddress, payment: payment, status: order.Status);

  }
}