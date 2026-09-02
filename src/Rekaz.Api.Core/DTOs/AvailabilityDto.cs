namespace Rekaz.Api.Core.DTOs;

public class AvailabilityDto
{
    public int ServiceId { get; set; }
    public string Date { get; set; } = string.Empty;
    public List<string> AvailableSlots { get; set; } = new();
    public string MessageAr { get; set; } = string.Empty;
    public string MessageEn { get; set; } = string.Empty;
}
