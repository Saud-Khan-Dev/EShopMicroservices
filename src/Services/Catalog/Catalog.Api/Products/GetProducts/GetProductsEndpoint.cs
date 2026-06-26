public record GetProductRequest(int? PageNumber = 1, int? PageSize = 10);

public record GetProductsResponse(IEnumerable<Product> Products);
public class GetProductsEndpoint : ICarterModule
{
  public void AddRoutes(IEndpointRouteBuilder app)
  {
    app.MapGet("/products", async ([AsParameters] GetProductRequest request, ISender sender) =>
    {
      var result = await sender.Send(new GetProductsQuery(request.PageNumber,request.PageSize));
      var response = result.Adapt<GetProductsResponse>();
      return Results.Ok(response);
    })
    .WithName("GetProducts")
    .Produces<GetProductsResponse>(StatusCodes.Status200OK)
    .ProducesProblem(StatusCodes.Status400BadRequest)
    .WithSummary("Get Products")
    .WithDescription("Get Products");
  }
}