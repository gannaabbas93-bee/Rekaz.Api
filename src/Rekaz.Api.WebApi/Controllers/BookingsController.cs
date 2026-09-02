namespace Rekaz.Api.WebApi.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rekaz.Api.Application.Interfaces;
using Rekaz.Api.Core.DTOs;

[ApiController]
[Route("api/[controller]")]
public class BookingsController : ControllerBase
{
    private readonly IServiceService _serviceService;

    public BookingsController(IServiceService serviceService)
    {
        _serviceService = serviceService;
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<ActionResult<BookingResponseDto>> CreateBooking([FromBody] CreateBookingDto dto, CancellationToken cancellationToken)
    {
        var response = await _serviceService.CreateBookingAsync(dto, cancellationToken);
        return Ok(response);
    }

    [HttpGet("history")]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<BookingHistoryDto>>> GetBookingHistory([FromQuery] string phone, CancellationToken cancellationToken)
    {
        var history = await _serviceService.GetBookingHistoryAsync(phone, cancellationToken);
        return Ok(history);
    }
}
