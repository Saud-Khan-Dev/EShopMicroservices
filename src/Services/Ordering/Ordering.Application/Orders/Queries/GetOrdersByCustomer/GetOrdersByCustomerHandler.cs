using BuildingBlocks.CQRS;
using Microsoft.EntityFrameworkCore;

public class GetOrdersByCustomerHandler(IApplicationDbContext dbContext) : IQueryHandler<GetOrdersByCustomer, GetOrdersByCustomerResult>
{
  public async Task<GetOrdersByCustomerResult> Handle(GetOrdersByCustomer query, CancellationToken cancellationToken)
  {
    var orders = await dbContext.Orders.Include(o => o.OrderItems)
    .AsNoTracking()
    .Where(o => o.CustomerId == CustomerId.Of(query.CustomerId)).OrderBy(o => o.OrderName.Value).ToListAsync(cancellationToken);
    return new GetOrdersByCustomerResult(orders.ToOrderDtoList());
  }
}