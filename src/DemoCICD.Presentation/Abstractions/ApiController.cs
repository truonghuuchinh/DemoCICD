using DemoCICD.Contract.Abstractions.Shared;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DemoCICD.Presentation.Abstractions;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
public abstract class ApiController : ControllerBase
{
    protected readonly ISender _sender;

    protected ApiController(ISender sender) => _sender = sender;

    protected IActionResult HandlerFailure(Result result) =>
        result switch
        {
            { IsSuccess: true } => throw new InvalidOperationException(),
            ValidationResult validationResult =>
                BadRequest(
                    CreateProblemDetails(
                        "Validation Error",
                        StatusCodes.Status400BadRequest,
                        result.Error,
                        validationResult.Errors)),
            _ =>
                BadRequest(
                    CreateProblemDetails(
                        "Bab Request",
                        StatusCodes.Status400BadRequest,
                        result.Error))
        };

    protected ProblemDetails CreateProblemDetails(
        string title,
        int status,
        Error? error,
        Error[]? errors = null)
    {
        return new ProblemDetails
        {
            Title = title,
            Type = error.Code,
            Status = status,
            Detail = error.Message,
            Extensions = { { nameof(errors), errors } }
        };
    }
}
