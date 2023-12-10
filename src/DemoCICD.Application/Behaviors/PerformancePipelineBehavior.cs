using System.Diagnostics;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DemoCICD.Application.Behaviors;
public sealed class PerformancePipelineBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly Stopwatch _timer;
    private readonly ILogger<TRequest> _logger;

    public PerformancePipelineBehavior(ILogger<TRequest> logger)
    {
        _timer = new Stopwatch();
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        _timer.Start();
        var response = await next();
        _timer.Stop();

        var elapsedMilliseconds = _timer.ElapsedMilliseconds;

        if (elapsedMilliseconds <= 5000)
        {
            _logger.LogInformation(
                "Request has been executed in {ElapsedMilliseconds} second",
                elapsedMilliseconds);
            return response;
        }

        var requestName = typeof(TRequest).Name;

        _logger.LogWarning(
            "Long Time Running - Request Details: {Name} ({ElapsedMilliseconds} milliseconds) {@Request}",
            requestName,
            elapsedMilliseconds,
            request);
        return response;
    }
}
