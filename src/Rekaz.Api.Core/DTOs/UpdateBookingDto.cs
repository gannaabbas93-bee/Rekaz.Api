namespace Rekaz.Api.Core.DTOs;

public class UpdateBookingDto
{
    public string FullName { get; set; } = string.Empty;
    public string BusinessType { get; set; } = string.Empty;
    public string CountryCode { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public int ServiceId { get; set; }
    public string BookingDate { get; set; } = string.Empty;
    public string SelectedSlot { get; set; } = string.Empty;
}
