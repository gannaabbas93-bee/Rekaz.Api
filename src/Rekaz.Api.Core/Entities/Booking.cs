namespace Rekaz.Api.Core.Entities;

public class Booking
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string BusinessType { get; set; } = string.Empty;
    public string CountryCode { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public int ServiceId { get; set; }
    public Service? Service { get; set; }
    public string BookingDate { get; set; } = string.Empty;
    public string SelectedSlot { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
