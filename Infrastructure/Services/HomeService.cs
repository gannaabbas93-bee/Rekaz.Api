namespace Rekaz.Api.Infrastructure.Services;

using Rekaz.Api.Core.DTOs;
using Rekaz.Api.Core.Interfaces;

public class HomeService : IHomeService
{
    private readonly IServiceService _serviceService;

    public HomeService(IServiceService serviceService)
    {
        _serviceService = serviceService;
    }

    public async Task<HomeDataDto> GetAggregatedHomeDataAsync(int? serviceId, string? date)
    {
        var sId = serviceId ?? 1;
        var targetDate = string.IsNullOrWhiteSpace(date) ? DateTime.Today.ToString("yyyy-MM-dd") : date;

        var slots = sId switch
        {
            2 => new List<string> { "09:00 AM", "11:30 AM", "02:00 PM", "04:30 PM", "07:00 PM" },
            3 => new List<string> { "10:30 AM", "01:00 PM", "03:30 PM", "06:00 PM" },
            4 => new List<string> { "09:30 AM", "12:00 PM", "02:30 PM", "05:00 PM", "08:30 PM" },
            _ => new List<string> { "10:00 AM", "12:00 PM", "03:00 PM", "05:30 PM", "08:00 PM" }
        };

        var servicesList = await _serviceService.GetActiveServicesAsync();

        return new HomeDataDto
        {
            Headline = "منصة إدارة الحجوزات والاشتراكات الذكية",
            Subtitle = "منصة إدارة الحجوزات والاشتراكات الذكية التي تمنح أعمالك الكفاءة والنمو",
            RotatingWords = new List<string> { "الحجوزات", "الاشتراكات", "Bookings", "Memberships" },
            Services = servicesList.ToList(),
            AvailableSlots = slots
        };
    }
}
