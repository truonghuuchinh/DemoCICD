using DemoCICD.Contract.Abstractions.Shared;
using MediatR;

namespace DemoCICD.Contract.Abstractions.Messages;
public interface ICommand : IRequest<Result>
{
}

public interface ICommand<TResponse> : IRequest<Result<TResponse>>
{
}
