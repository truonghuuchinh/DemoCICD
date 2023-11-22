using DemoCICD.Contract.Abstractions.Shared;
using MediatR;

namespace DemoCICD.Contract.Abstractions.Messages;
public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>
{
}

