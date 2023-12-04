using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DemoCICD.Presentation.Abstractions;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
public abstract class ApiController : ControllerBase
{
    protected readonly ISender _sender;

    protected ApiController(ISender sender) => _sender = sender;
}
