namespace Rekaz.Api.Core.DTOs;

public class HomeDataDto
{
    public string TitleAr { get; set; } = "أهلاً بك في ركاز";
    public string TitleEn { get; set; } = "Welcome to Rekaz";
    public string MessageAr { get; set; } = "منصة إدارة الحجوزات والاشتراكات الذكية التي تمنح أعمالك الكفاءة والنمو";
    public string MessageEn { get; set; } = "Smart Bookings & Memberships Management Platform driving growth and efficiency";
    public IEnumerable<ServiceDto> Services { get; set; } = new List<ServiceDto>();
    public DateTime ServerTime { get; set; } = DateTime.UtcNow;
    public string BackendVersion { get; set; } = "NET 10.0 API (Clean Architecture)";
}
