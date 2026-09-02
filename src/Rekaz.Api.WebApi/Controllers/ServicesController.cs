namespace Rekaz.Api.WebApi.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rekaz.Api.Application.Interfaces;
using Rekaz.Api.Core.DTOs;

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
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<ServiceDto>>> GetServices(CancellationToken cancellationToken)
    {
        var services = await _serviceService.GetActiveServicesAsync(cancellationToken);
        return Ok(services);
    }
}
