using MediatR;
using OrderManagement.Domain.Abstractions;

namespace OrderManagement.Application.Abstractions.Messaging;

public interface ICommand : IRequest<Result> { }
public interface ICommand<TResponse> : IRequest<Result<TResponse>> { }

