namespace Rekaz.Api.Core.DTOs;

public class BookingHistoryDto
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string BusinessType { get; set; } = string.Empty;
    public string ServiceNameAr { get; set; } = string.Empty;
    public string ServiceNameEn { get; set; } = string.Empty;
    public string ServiceIcon { get; set; } = string.Empty;
    public string BookingDate { get; set; } = string.Empty;
    public string SelectedSlot { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}
