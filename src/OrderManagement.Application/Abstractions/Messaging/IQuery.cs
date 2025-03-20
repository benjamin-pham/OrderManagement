using MediatR;
using OrderManagement.Domain.Abstractions;

namespace OrderManagement.Application.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}
