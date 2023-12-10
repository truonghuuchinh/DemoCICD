using MediatR;
using Microsoft.Extensions.Logging;

namespace DemoCICD.Application.Behaviors;

public class TracingPipelineBehavior<TRequest, TResponse> :
    IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly ILogger<TRequest> _logger;

    public TracingPipelineBehavior(ILogger<TRequest> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Start handle request with body: {@Request}",
            request!);

        var response = await next();

        _logger.LogInformation(
            "Finish Excute request with response: {@Response}",
            response);
        return response;
    }
}
