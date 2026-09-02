namespace Rekaz.Api.Application.Services;

using Rekaz.Api.Application.Interfaces;
using Rekaz.Api.Core.DTOs;

public class HomeService : IHomeService
{
    private readonly IServiceService _serviceService;

    public HomeService(IServiceService serviceService)
    {
        _serviceService = serviceService;
    }

    public async Task<HomeDataDto> GetAggregatedHomeDataAsync(int? serviceId, string? date, CancellationToken cancellationToken = default)
    {
        var services = await _serviceService.GetActiveServicesAsync(cancellationToken);
        return new HomeDataDto
        {
            TitleAr = "أهلاً بك في ركاز",
            TitleEn = "Welcome to Rekaz",
            MessageAr = "منصة إدارة الحجوزات والاشتراكات الذكية التي تمنح أعمالك الكفاءة والنمو",
            MessageEn = "Smart Bookings & Memberships Management Platform driving growth and efficiency",
            Services = services,
            ServerTime = DateTime.UtcNow,
            BackendVersion = "NET 10.0 API (Clean Architecture)"
        };
    }
}
