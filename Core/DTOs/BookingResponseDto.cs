namespace Rekaz.Api.Core.DTOs;

public class BookingResponseDto
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public int ServiceId { get; set; }
    public string BookingDate { get; set; } = string.Empty;
    public string SelectedSlot { get; set; } = string.Empty;
    public string MessageAr { get; set; } = string.Empty;
    public string MessageEn { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}
