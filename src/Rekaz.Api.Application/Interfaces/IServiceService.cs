namespace Rekaz.Api.Application.Interfaces;

using Rekaz.Api.Core.DTOs;

public interface IServiceService
{
    Task<IEnumerable<ServiceDto>> GetActiveServicesAsync(CancellationToken cancellationToken = default);
    Task<AvailabilityDto> GetAvailabilityAsync(int serviceId, string date, CancellationToken cancellationToken = default);
    Task<BookingResponseDto> CreateBookingAsync(CreateBookingDto dto, CancellationToken cancellationToken = default);
    Task<IEnumerable<BookingHistoryDto>> GetBookingHistoryAsync(string phone, CancellationToken cancellationToken = default);
}
