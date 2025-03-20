using MediatR;
using OrderManagement.Domain.Abstractions;

namespace OrderManagement.Application.Abstractions.Messaging;

public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>> where TQuery : IQuery<TResponse>
{
}