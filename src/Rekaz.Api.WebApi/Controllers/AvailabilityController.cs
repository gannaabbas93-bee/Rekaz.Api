namespace Rekaz.Api.WebApi.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rekaz.Api.Application.Interfaces;
using Rekaz.Api.Core.DTOs;

[ApiController]
[Route("api/[controller]")]
public class AvailabilityController : ControllerBase
{
    private readonly IServiceService _serviceService;

    public AvailabilityController(IServiceService serviceService)
    {
        _serviceService = serviceService;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<AvailabilityDto>> GetAvailability([FromQuery] int serviceId, [FromQuery] string date, CancellationToken cancellationToken)
    {
        var availability = await _serviceService.GetAvailabilityAsync(serviceId, date, cancellationToken);
        return Ok(availability);
    }
}
