namespace Rekaz.Api.Core.Interfaces;

using Rekaz.Api.Core.DTOs;

public interface IServiceService
{
    Task<IEnumerable<ServiceDto>> GetActiveServicesAsync();
    Task<AvailabilityDto> GetAvailabilityAsync(int serviceId, string date);
    Task<BookingResponseDto> CreateBookingAsync(CreateBookingDto dto);
    Task<IEnumerable<BookingHistoryDto>> GetBookingHistoryAsync(string phone);
}
