using BuildingBlocks.CQRS;

public record GetOrdersByCustomer(Guid CustomerId) : IQuery<GetOrdersByCustomerResult>;

public record GetOrdersByCustomerResult(IEnumerable<OrderDto> Orders);