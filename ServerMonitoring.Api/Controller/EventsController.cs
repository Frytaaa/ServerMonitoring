using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ServerMonitoring.Api.Controller;

[ApiController]
public class EventsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [Route("index")]
    public async Task<IActionResult> Index()
    {
        return Ok();
    }
}