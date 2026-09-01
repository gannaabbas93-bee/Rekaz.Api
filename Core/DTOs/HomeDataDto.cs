namespace Rekaz.Api.Core.DTOs;

public class HomeDataDto
{
    public string Headline { get; set; } = string.Empty;
    public string Subtitle { get; set; } = string.Empty;
    public List<string> RotatingWords { get; set; } = new();
    public List<ServiceDto> Services { get; set; } = new();
    public List<string> AvailableSlots { get; set; } = new();
}
