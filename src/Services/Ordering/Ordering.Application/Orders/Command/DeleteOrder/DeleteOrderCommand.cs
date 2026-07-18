using System.Runtime.CompilerServices;
using BuildingBlocks.CQRS;
using FluentValidation;

public record DeleteOrderCommand(Guid OrderId) : ICommand<DeleteOrderCommandResult>;

public record DeleteOrderCommandResult(bool IsSuccess);

public class DeleteOrderCommandValidator:AbstractValidator<DeleteOrderCommand>
{
  public DeleteOrderCommandValidator()
  {
    RuleFor(x=>x.OrderId).NotEmpty().WithMessage("Order id is required");
  }
}