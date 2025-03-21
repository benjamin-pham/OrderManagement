using FluentValidation;

namespace OrderManagement.Application.UseCases.Orders.CreateOrder;
internal class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(x => x.CustomerName)
            .NotEmpty()
            .MaximumLength(255);
    }
}
