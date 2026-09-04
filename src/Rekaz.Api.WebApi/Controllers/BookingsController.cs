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

    [HttpPut("{id}")]
    [AllowAnonymous]
    public async Task<ActionResult<BookingResponseDto>> UpdateBooking(int id, [FromBody] UpdateBookingDto dto, CancellationToken cancellationToken)
    {
        var response = await _serviceService.UpdateBookingAsync(id, dto, cancellationToken);
        if (response == null)
        {
            return NotFound(new { message = "Booking not found." });
        }
        return Ok(response);
    }

    [HttpDelete("{id}")]
    [AllowAnonymous]
    public async Task<IActionResult> DeleteBooking(int id, CancellationToken cancellationToken)
    {
        var success = await _serviceService.DeleteBookingAsync(id, cancellationToken);
        if (!success)
        {
            return NotFound(new { message = "Booking not found." });
        }
        return Ok(new { message = "Booking deleted successfully." });
    }
}
