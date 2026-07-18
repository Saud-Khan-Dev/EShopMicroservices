using BuildingBlocks.CQRS;

public class DeleteOrderHandler(IApplicationDbContext dbContext) : ICommandHandler<DeleteOrderCommand, DeleteOrderCommandResult>
{
  public async Task<DeleteOrderCommandResult> Handle(DeleteOrderCommand command, CancellationToken cancellationToken)
  {
    var orderId = OrderId.Of(command.OrderId);
    var order = await dbContext.Orders.FindAsync([orderId], cancellationToken);
    if (order is null) throw new OrderNotFoundException(command.OrderId);

    dbContext.Orders.Remove(order);

    await dbContext.SaveChangesAsync(cancellationToken);


    return new DeleteOrderCommandResult(true);

  }
}