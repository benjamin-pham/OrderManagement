using FluentValidation;

namespace OrderManagement.Application.UseCases.Orders.DeleteOrder;
internal class DeleteOrderCommandValidator : AbstractValidator<DeleteOrderCommand>
{
    public DeleteOrderCommandValidator()
    {
        RuleFor(x => x.Id)
           .Must(x => x > 0);
    }
}
