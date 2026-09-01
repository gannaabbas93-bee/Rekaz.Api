namespace Rekaz.Api.Core.DTOs;

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
