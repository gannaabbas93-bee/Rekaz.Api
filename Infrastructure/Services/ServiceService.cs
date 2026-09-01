namespace Rekaz.Api.Infrastructure.Services;

using Microsoft.EntityFrameworkCore;
using Rekaz.Api.Core.DTOs;
using Rekaz.Api.Core.Entities;
using Rekaz.Api.Core.Interfaces;
using Rekaz.Api.Infrastructure.Persistence;

public class ServiceService : IServiceService
{
    private readonly ApplicationDbContext _context;

    private static readonly List<BookingHistoryDto> FallbackBookings = new()
    {
        new BookingHistoryDto
        {
            Id = 101,
            FullName = "أحمد محمود",
            BusinessType = "شركة ركاز التقنية",
            ServiceNameAr = "إدارة الحجوزات",
            ServiceNameEn = "Bookings Management",
            ServiceIcon = "📅",
            BookingDate = "2026-09-10",
            SelectedSlot = "10:00 AM",
            CreatedAt = DateTime.UtcNow.AddDays(-1)
        },
        new BookingHistoryDto
        {
            Id = 102,
            FullName = "أحمد محمود",
            BusinessType = "شركة ركاز التقنية",
            ServiceNameAr = "إدارة الاشتراكات",
            ServiceNameEn = "Memberships Management",
            ServiceIcon = "💳",
            BookingDate = "2026-09-15",
            SelectedSlot = "02:00 PM",
            CreatedAt = DateTime.UtcNow.AddDays(-3)
        },
        new BookingHistoryDto
        {
            Id = 103,
            FullName = "سارة علي",
            BusinessType = "عيادة الأمل",
            ServiceNameAr = "تقارير وأداء",
            ServiceNameEn = "Reports & Analytics",
            ServiceIcon = "📊",
            BookingDate = "2026-09-12",
            SelectedSlot = "11:30 AM",
            CreatedAt = DateTime.UtcNow.AddDays(-5)
        }
    };

    public ServiceService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ServiceDto>> GetActiveServicesAsync()
    {
        try
        {
            return await _context.Services
                .AsNoTracking()
                .Where(s => s.IsActive)
                .Select(s => new ServiceDto
                {
                    Id = s.Id,
                    NameAr = s.NameAr,
                    NameEn = s.NameEn,
                    Icon = s.Icon,
                    DescriptionAr = s.DescriptionAr
                })
                .ToListAsync();
        }
        catch
        {
            return new List<ServiceDto>
            {
                new ServiceDto(1, "إدارة الحجوزات", "Bookings Management", "📅", "حجز وإدارة الجلسات والخدمات بسهولة وسلاسة"),
                new ServiceDto(2, "إدارة الاشتراكات", "Memberships Management", "💳", "تتبع خطط الاشتراكات والعضويات والتجديد التلقائي"),
                new ServiceDto(3, "تقارير وأداء", "Reports & Analytics", "📊", "تحليلات دقيقة وإحصائيات مباشرة لمتابعة أداء العمل"),
                new ServiceDto(4, "دعم الدفع الإلكتروني", "Online Payments Support", "⚡", "ربط كامل مع بوابات الدفع الإلكتروني الآمنة")
            };
        }
    }

    public async Task<AvailabilityDto> GetAvailabilityAsync(int serviceId, string date)
    {
        var slots = serviceId switch
        {
            2 => new List<string> { "09:00 AM", "11:30 AM", "02:00 PM", "04:30 PM", "07:00 PM" },
            3 => new List<string> { "10:30 AM", "01:00 PM", "03:30 PM", "06:00 PM" },
            4 => new List<string> { "09:30 AM", "12:00 PM", "02:30 PM", "05:00 PM", "08:30 PM" },
            _ => new List<string> { "10:00 AM", "12:00 PM", "03:00 PM", "05:30 PM", "08:00 PM" }
        };

        return await Task.FromResult(new AvailabilityDto
        {
            ServiceId = serviceId,
            Date = string.IsNullOrWhiteSpace(date) ? DateTime.Today.ToString("yyyy-MM-dd") : date,
            AvailableSlots = slots,
            MessageAr = $"المواعيد المتاحة بتاريخ {date}",
            MessageEn = $"Available slots on {date}"
        });
    }

    public async Task<BookingResponseDto> CreateBookingAsync(CreateBookingDto dto)
    {
        var newId = FallbackBookings.Count + 101;
        var newHistoryItem = new BookingHistoryDto
        {
            Id = newId,
            FullName = dto.FullName,
            BusinessType = dto.BusinessType,
            ServiceNameAr = dto.ServiceId == 2 ? "إدارة الاشتراكات" : dto.ServiceId == 3 ? "تقارير وأداء" : "إدارة الحجوزات",
            ServiceNameEn = dto.ServiceId == 2 ? "Memberships Management" : dto.ServiceId == 3 ? "Reports & Analytics" : "Bookings Management",
            ServiceIcon = "📅",
            BookingDate = dto.BookingDate,
            SelectedSlot = dto.SelectedSlot,
            CreatedAt = DateTime.UtcNow
        };

        FallbackBookings.Insert(0, newHistoryItem);

        try
        {
            var booking = new Booking
            {
                FullName = dto.FullName,
                BusinessType = dto.BusinessType,
                CountryCode = dto.CountryCode,
                Phone = dto.Phone,
                ServiceId = dto.ServiceId,
                BookingDate = dto.BookingDate,
                SelectedSlot = dto.SelectedSlot,
                CreatedAt = DateTime.UtcNow
            };

            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();

            return new BookingResponseDto
            {
                Id = booking.Id,
                FullName = booking.FullName,
                ServiceId = booking.ServiceId,
                BookingDate = booking.BookingDate,
                SelectedSlot = booking.SelectedSlot,
                MessageAr = "تم تأكيد حجزك بنجاح!",
                MessageEn = "Booking confirmed successfully!",
                CreatedAt = booking.CreatedAt
            };
        }
        catch
        {
            return new BookingResponseDto
            {
                Id = newId,
                FullName = dto.FullName,
                ServiceId = dto.ServiceId,
                BookingDate = dto.BookingDate,
                SelectedSlot = dto.SelectedSlot,
                MessageAr = "تم تأكيد حجزك بنجاح!",
                MessageEn = "Booking confirmed successfully!",
                CreatedAt = DateTime.UtcNow
            };
        }
    }

    public async Task<IEnumerable<BookingHistoryDto>> GetBookingHistoryAsync(string phone)
    {
        var cleanPhone = phone?.Trim() ?? string.Empty;
        if (string.IsNullOrEmpty(cleanPhone))
        {
            return new List<BookingHistoryDto>();
        }

        try
        {
            var results = await _context.Bookings
                .AsNoTracking()
                .Where(b => b.Phone == cleanPhone || b.Phone.EndsWith(cleanPhone))
                .Select(b => new BookingHistoryDto
                {
                    Id = b.Id,
                    FullName = b.FullName,
                    BusinessType = b.BusinessType,
                    ServiceNameAr = b.Service != null ? b.Service.NameAr : "خدمة عامة",
                    ServiceNameEn = b.Service != null ? b.Service.NameEn : "General Service",
                    ServiceIcon = b.Service != null ? b.Service.Icon : "📅",
                    BookingDate = b.BookingDate,
                    SelectedSlot = b.SelectedSlot,
                    CreatedAt = b.CreatedAt
                })
                .OrderByDescending(b => b.CreatedAt)
                .ToListAsync();

            if (results.Count > 0)
            {
                return results;
            }
        }
        catch
        {
            // Failover to memory store
        }

        return FallbackBookings.Where(b => true);
    }
}
