namespace Rekaz.Api.Controllers;

using Microsoft.AspNetCore.Mvc;
using Rekaz.Api.Core.DTOs;
using Rekaz.Api.Core.Interfaces;

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
    public async Task<ActionResult<AvailabilityDto>> GetAvailability([FromQuery] int serviceId, [FromQuery] string date)
    {
        if (serviceId <= 0)
        {
            return BadRequest("serviceId must be greater than 0.");
        }

        var result = await _serviceService.GetAvailabilityAsync(serviceId, date);
        return Ok(result);
    }
}
