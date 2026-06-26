public record GetBasketQuery(string UserName) : IQuery<GetBasketQueryResult>;
public record GetBasketQueryResult(ShoppingCart Cart);

public class GetBasketQueryHandler(IBasketRepository repository) : IQueryHandler<GetBasketQuery, GetBasketQueryResult>
{
  public async Task<GetBasketQueryResult> Handle(GetBasketQuery query, CancellationToken cancellationToken)
  {
    var basket = await repository.GetBasket(query.UserName);
    return new GetBasketQueryResult(basket);
  }
} 