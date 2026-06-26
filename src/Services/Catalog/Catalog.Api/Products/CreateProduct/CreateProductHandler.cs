
public record CreateProductCommand(
string Name,
List<string> Category,
string Description,
string ImageFile,
decimal Price
) : ICommand<CreateProductResult>;

public record CreateProductResult(
Guid Id
);

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
  public CreateProductCommandValidator()
  {
    RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Product name is required.")
            .MaximumLength(100);

    RuleFor(x => x.Category)
        .NotNull()
        .WithMessage("Category is required.")
        .Must(x => x.Any())
        .WithMessage("At least one category is required.");

    RuleForEach(x => x.Category)
        .NotEmpty()
        .WithMessage("Category name cannot be empty.");

    RuleFor(x => x.Description)
        .NotEmpty()
        .WithMessage("Description is required.")
        .MaximumLength(1000);

    RuleFor(x => x.ImageFile)
        .NotEmpty()
        .WithMessage("Image file is required.");

    RuleFor(x => x.Price)
        .GreaterThan(0)
        .WithMessage("Price must be greater than zero.");
  }
}


internal class CreateProductCommandHandler(IDocumentSession session) : ICommandHandler<CreateProductCommand, CreateProductResult>
{
  public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
  {


    //Create Product 
    var product = new Product
    {
      Name = command.Name,
      Category = command.Category,
      Description = command.Description,
      ImageFile = command.ImageFile,
      Price = command.Price
    };

    //Save in DB
    session.Store(product);
    await session.SaveChangesAsync(cancellationToken);

    //return Result 
    return new CreateProductResult(product.Id);
  }
}