namespace Rekaz.Api.Controllers;

using Microsoft.AspNetCore.Mvc;
using Rekaz.Api.Core.DTOs;
using Rekaz.Api.Core.Interfaces;

[ApiController]
[Route("api/[controller]")]
public class ServicesController : ControllerBase
{
    private readonly IServiceService _serviceService;

    public ServicesController(IServiceService serviceService)
    {
        _serviceService = serviceService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ServiceDto>>> GetServices()
    {
        var services = await _serviceService.GetActiveServicesAsync();
        return Ok(services);
    }
}
