using FluentValidation;

namespace OrderManagement.Application.UseCases.Orders.UpdateOrder;
internal class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
{
    public UpdateOrderCommandValidator()
    {
        RuleFor(x => x.Id)
            .Must(x => x > 0);

        RuleFor(x => x.CustomerName)
            .NotEmpty()
            .MaximumLength(255);
    }
}
