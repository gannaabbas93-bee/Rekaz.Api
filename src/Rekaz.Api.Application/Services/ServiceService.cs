namespace Rekaz.Api.Application.Services;

using Rekaz.Api.Application.Interfaces;
using Rekaz.Api.Core.DTOs;
using Rekaz.Api.Core.Entities;
using Rekaz.Api.Core.Interfaces;

public class ServiceService : IServiceService
{
    private readonly IServiceRepository _serviceRepository;
    private readonly IBookingRepository _bookingRepository;

    public ServiceService(IServiceRepository serviceRepository, IBookingRepository bookingRepository)
    {
        _serviceRepository = serviceRepository;
        _bookingRepository = bookingRepository;
    }

    public async Task<IEnumerable<ServiceDto>> GetActiveServicesAsync(CancellationToken cancellationToken = default)
    {
        var services = await _serviceRepository.GetActiveServicesAsync(cancellationToken);
        return services.Select(s => new ServiceDto
        {
            Id = s.Id,
            NameAr = s.NameAr,
            NameEn = s.NameEn,
            Icon = s.Icon,
            DescriptionAr = s.DescriptionAr
        });
    }

    public async Task<AvailabilityDto> GetAvailabilityAsync(int serviceId, string date, CancellationToken cancellationToken = default)
    {
        var slots = serviceId switch
        {
            2 => new List<string> { "09:00 AM", "11:30 AM", "02:00 PM", "04:30 PM", "07:00 PM" },
            3 => new List<string> { "10:30 AM", "01:00 PM", "03:30 PM", "06:00 PM" },
            4 => new List<string> { "09:30 AM", "12:00 PM", "02:30 PM", "05:00 PM", "08:30 PM" },
            _ => new List<string> { "10:00 AM", "12:00 PM", "03:00 PM", "05:30 PM", "08:00 PM" }
        };

        var targetDate = string.IsNullOrWhiteSpace(date) ? DateTime.Today.ToString("yyyy-MM-dd") : date;

        return await Task.FromResult(new AvailabilityDto
        {
            ServiceId = serviceId,
            Date = targetDate,
            AvailableSlots = slots,
            MessageAr = $"المواعيد المتاحة بتاريخ {targetDate}",
            MessageEn = $"Available slots on {targetDate}"
        });
    }

    public async Task<BookingResponseDto> CreateBookingAsync(CreateBookingDto dto, CancellationToken cancellationToken = default)
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

        var createdBooking = await _bookingRepository.AddAsync(booking, cancellationToken);

        return new BookingResponseDto
        {
            Id = createdBooking.Id,
            FullName = createdBooking.FullName,
            ServiceId = createdBooking.ServiceId,
            BookingDate = createdBooking.BookingDate,
            SelectedSlot = createdBooking.SelectedSlot,
            MessageAr = "تم تأكيد حجزك بنجاح!",
            MessageEn = "Booking confirmed successfully!",
            CreatedAt = createdBooking.CreatedAt
        };
    }

    public async Task<IEnumerable<BookingHistoryDto>> GetBookingHistoryAsync(string phone, CancellationToken cancellationToken = default)
    {
        var cleanPhone = phone?.Trim() ?? string.Empty;
        if (string.IsNullOrEmpty(cleanPhone))
        {
            return new List<BookingHistoryDto>();
        }

        var bookings = await _bookingRepository.GetHistoryByPhoneAsync(cleanPhone, cancellationToken);
        return bookings.Select(b => new BookingHistoryDto
        {
            Id = b.Id,
            FullName = b.FullName,
            BusinessType = b.BusinessType,
            Phone = b.Phone,
            ServiceNameAr = b.Service != null ? b.Service.NameAr : "خدمة عامة",
            ServiceNameEn = b.Service != null ? b.Service.NameEn : "General Service",
            ServiceIcon = b.Service != null ? b.Service.Icon : "📅",
            BookingDate = b.BookingDate,
            SelectedSlot = b.SelectedSlot,
            CreatedAt = b.CreatedAt
        });
    }
}
