namespace Rekaz.Api.Core.Interfaces;

using Rekaz.Api.Core.Entities;

public interface IBookingRepository
{
    Task<Booking> AddAsync(Booking booking, CancellationToken cancellationToken = default);
    Task<IEnumerable<Booking>> GetHistoryByPhoneAsync(string phone, CancellationToken cancellationToken = default);
}
