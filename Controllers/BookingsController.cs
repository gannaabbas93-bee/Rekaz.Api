namespace Rekaz.Api.Controllers;

using Microsoft.AspNetCore.Mvc;
using Rekaz.Api.Core.DTOs;
using Rekaz.Api.Core.Interfaces;

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
    public async Task<ActionResult<BookingResponseDto>> CreateBooking([FromBody] CreateBookingDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var response = await _serviceService.CreateBookingAsync(dto);
            return CreatedAtAction(nameof(CreateBooking), new { id = response.Id }, response);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet("history")]
    public async Task<ActionResult<IEnumerable<BookingHistoryDto>>> GetBookingHistory([FromQuery] string phone)
    {
        if (string.IsNullOrWhiteSpace(phone))
        {
            return BadRequest(new { message = "Phone number is required." });
        }

        var history = await _serviceService.GetBookingHistoryAsync(phone);
        return Ok(history);
    }
}
