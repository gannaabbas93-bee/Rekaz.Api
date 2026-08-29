namespace Rekaz.Api.Controllers;

using Microsoft.AspNetCore.Mvc;
using Rekaz.Api.Core.DTOs;
using Rekaz.Api.Core.Interfaces;

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
    public async Task<ActionResult<HomeDataDto>> GetHomeData([FromQuery] int? serviceId, [FromQuery] string? date)
    {
        var data = await _homeService.GetAggregatedHomeDataAsync(serviceId, date);
        return Ok(data);
    }
}
