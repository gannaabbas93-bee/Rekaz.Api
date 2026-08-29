namespace Rekaz.Api.Core.DTOs;

public class HomeDataDto
{
    public string Headline { get; set; } = string.Empty;
    public string Subtitle { get; set; } = string.Empty;
    public List<string> RotatingWords { get; set; } = new();
    public List<ServiceDto> Services { get; set; } = new();
    public List<string> AvailableSlots { get; set; } = new();
}

public class ServiceDto
{
    public int Id { get; set; }
    public string NameAr { get; set; } = string.Empty;
    public string NameEn { get; set; } = string.Empty;
    public string Icon { get; set; } = string.Empty;
    public string DescriptionAr { get; set; } = string.Empty;

    public ServiceDto() { }

    public ServiceDto(int id, string nameAr, string nameEn, string icon, string descriptionAr)
    {
        Id = id;
        NameAr = nameAr;
        NameEn = nameEn;
        Icon = icon;
        DescriptionAr = descriptionAr;
    }
}
