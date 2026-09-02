namespace Rekaz.Api.Infrastructure.Repositories;

using Microsoft.EntityFrameworkCore;
using Rekaz.Api.Core.Entities;
using Rekaz.Api.Core.Interfaces;
using Rekaz.Api.Infrastructure.Persistence;

public class ServiceRepository : IServiceRepository
{
    private readonly ApplicationDbContext _context;

    private static readonly List<Service> FallbackServices = new()
    {
        new Service { Id = 1, NameAr = "إدارة الحجوزات", NameEn = "Bookings Management", Icon = "📅", DescriptionAr = "حجز وإدارة الجلسات والخدمات بسهولة وسلاسة", IsActive = true },
        new Service { Id = 2, NameAr = "إدارة الاشتراكات", NameEn = "Memberships Management", Icon = "💳", DescriptionAr = "تتبع خطط الاشتراكات والعضويات والتجديد التلقائي", IsActive = true },
        new Service { Id = 3, NameAr = "تقارير وأداء", NameEn = "Reports & Analytics", Icon = "📊", DescriptionAr = "تحليلات دقيقة وإحصائيات مباشرة لمتابعة أداء العمل", IsActive = true },
        new Service { Id = 4, NameAr = "دعم الدفع الإلكتروني", NameEn = "Online Payments Support", Icon = "⚡", DescriptionAr = "ربط كامل مع بوابات الدفع الإلكتروني الآمنة", IsActive = true }
    };

    public ServiceRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Service>> GetActiveServicesAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var services = await _context.Services
                .AsNoTracking()
                .Where(s => s.IsActive)
                .ToListAsync(cancellationToken);

            if (services.Count > 0)
            {
                return services;
            }
        }
        catch
        {
            // Failover to memory store
        }

        return FallbackServices;
    }

    public async Task<Service?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            var service = await _context.Services
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);

            if (service != null)
            {
                return service;
            }
        }
        catch
        {
            // Failover
        }

        return FallbackServices.FirstOrDefault(s => s.Id == id);
    }
}
