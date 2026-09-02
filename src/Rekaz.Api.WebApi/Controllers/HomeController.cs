namespace Rekaz.Api.WebApi.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rekaz.Api.Application.Interfaces;
using Rekaz.Api.Core.DTOs;

[ApiController]
[Route("api/[controller]")]
public class HomeController : ControllerBase
{
    private readonly IHomeService _homeService;

    public HomeController(IHomeService homeService)
    {
        _homeService = homeService;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<HomeDataDto>> GetHomeData([FromQuery] int? serviceId, [FromQuery] string? date, CancellationToken cancellationToken)
    {
        var data = await _homeService.GetAggregatedHomeDataAsync(serviceId, date, cancellationToken);
        return Ok(data);
    }
}
