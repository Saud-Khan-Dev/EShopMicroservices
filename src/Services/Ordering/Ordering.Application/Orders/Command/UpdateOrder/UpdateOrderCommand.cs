using BuildingBlocks.CQRS;
using FluentValidation;

public record UpdateOrderCommand(OrderDto Order) : ICommand<UpdateOrderResult>;

public record UpdateOrderResult(bool IsSuccess);

public class UpdateOrderCommandValidation:AbstractValidator<UpdateOrderCommand>
{
   public UpdateOrderCommandValidation(){
    RuleFor(x => x.Order.Id).NotEmpty().WithMessage("Id is required");
    RuleFor(x => x.Order.CustomerId).NotEmpty().WithMessage("CustomerId is required");
    RuleFor(x => x.Order.OrderName).NotEmpty().WithMessage("Order name should not be empty");

  }
} 