using FluentValidation;
using MediatR;
using OrderManagement.Domain.Exceptions;
using System.Windows.Input;

namespace OrderManagement.Application.Abstractions.Behaviors;

internal class ValidationBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : ICommand
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        this._validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (!_validators.Any())
        {
            return await next();
        }

        ValidationContext<TRequest> context = new ValidationContext<TRequest>(request);

        List<string> validationErrors = _validators
            .Select(validator => validator.Validate(context))
            .Where(validationResult => validationResult.Errors.Any())
            .SelectMany(validationResult => validationResult.Errors)
            .Select(validationFailure => validationFailure.ErrorMessage)
            .ToList();

        if (validationErrors.Any())
        {
            throw new AppException(string.Join("; ", validationErrors));
        }

        return await next();
    }
}
