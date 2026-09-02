namespace Rekaz.Api.Infrastructure.Repositories;

using Microsoft.EntityFrameworkCore;
using Rekaz.Api.Core.Entities;
using Rekaz.Api.Core.Interfaces;
using Rekaz.Api.Infrastructure.Persistence;

public class BookingRepository : IBookingRepository
{
    private readonly ApplicationDbContext _context;

    private static readonly List<Booking> FallbackBookings = new()
    {
        new Booking
        {
            Id = 101,
            FullName = "أحمد محمود",
            BusinessType = "شركة ركاز التقنية",
            CountryCode = "+966",
            Phone = "0501234567",
            ServiceId = 1,
            Service = new Service { Id = 1, NameAr = "إدارة الحجوزات", NameEn = "Bookings Management", Icon = "📅" },
            BookingDate = "2026-09-10",
            SelectedSlot = "10:00 AM",
            CreatedAt = DateTime.UtcNow.AddDays(-1)
        },
        new Booking
        {
            Id = 102,
            FullName = "أحمد محمود",
            BusinessType = "شركة ركاز التقنية",
            CountryCode = "+966",
            Phone = "0501234567",
            ServiceId = 2,
            Service = new Service { Id = 2, NameAr = "إدارة الاشتراكات", NameEn = "Memberships Management", Icon = "💳" },
            BookingDate = "2026-09-15",
            SelectedSlot = "02:00 PM",
            CreatedAt = DateTime.UtcNow.AddDays(-3)
        },
        new Booking
        {
            Id = 103,
            FullName = "سارة علي",
            BusinessType = "عيادة الأمل",
            CountryCode = "+966",
            Phone = "0555555555",
            ServiceId = 3,
            Service = new Service { Id = 3, NameAr = "تقارير وأداء", NameEn = "Reports & Analytics", Icon = "📊" },
            BookingDate = "2026-09-12",
            SelectedSlot = "11:30 AM",
            CreatedAt = DateTime.UtcNow.AddDays(-5)
        }
    };

    public BookingRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Booking> AddAsync(Booking booking, CancellationToken cancellationToken = default)
    {
        try
        {
            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync(cancellationToken);
        }
        catch
        {
            booking.Id = FallbackBookings.Count + 101;
        }

        FallbackBookings.Insert(0, booking);
        return booking;
    }

    public async Task<IEnumerable<Booking>> GetHistoryByPhoneAsync(string phone, CancellationToken cancellationToken = default)
    {
        var cleanPhone = phone?.Trim() ?? string.Empty;
        if (string.IsNullOrEmpty(cleanPhone))
        {
            return new List<Booking>();
        }

        try
        {
            var results = await _context.Bookings
                .AsNoTracking()
                .Include(b => b.Service)
                .Where(b => b.Phone == cleanPhone || b.Phone.EndsWith(cleanPhone) || cleanPhone.EndsWith(b.Phone))
                .OrderByDescending(b => b.CreatedAt)
                .ToListAsync(cancellationToken);

            if (results.Count > 0)
            {
                return results;
            }
        }
        catch
        {
            // Failover to memory store
        }

        return FallbackBookings
            .Where(b => b.Phone == cleanPhone || b.Phone.EndsWith(cleanPhone) || cleanPhone.EndsWith(b.Phone))
            .OrderByDescending(b => b.CreatedAt)
            .ToList();
    }
}
