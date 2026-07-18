using BuildingBlocks.CQRS;

public record GetOrdersByNameQuery(string OrderName) : IQuery<GetOrdersByNameResult>;

public record GetOrdersByNameResult(IEnumerable<OrderDto> Orders);

