using DemoCICD.Contract.Abstractions.Shared;
using MediatR;

namespace DemoCICD.Contract.Abstractions.Messages;
public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}
