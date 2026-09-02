namespace Rekaz.Api.Infrastructure.Persistence;

using Rekaz.Api.Core.Entities;
using Rekaz.Api.Core.Enums;
using Rekaz.Api.Core.Interfaces;

public static class DbInitializer
{
    public static void Seed(ApplicationDbContext context, IPasswordHasher passwordHasher)
    {
        try
        {
            context.Database.EnsureCreated();

            if (!context.Users.Any())
            {
                context.Users.Add(new User
                {
                    FullName = "System Admin",
                    Email = "admin@rekaz.com",
                    PasswordHash = passwordHasher.HashPassword("Admin123!"),
                    Role = UserRole.Admin,
                    CreatedAt = DateTime.UtcNow
                });
                context.SaveChanges();
            }

            if (!context.Services.Any())
            {
                context.Services.AddRange(
                    new Service { NameAr = "إدارة الحجوزات", NameEn = "Bookings Management", Icon = "📅", DescriptionAr = "حجز وإدارة الجلسات والخدمات بسهولة وسلاسة", IsActive = true },
                    new Service { NameAr = "إدارة الاشتراكات", NameEn = "Memberships Management", Icon = "💳", DescriptionAr = "تتبع خطط الاشتراكات والعضويات والتجديد التلقائي", IsActive = true },
                    new Service { NameAr = "تقارير وأداء", NameEn = "Reports & Analytics", Icon = "📊", DescriptionAr = "تحليلات دقيقة وإحصائيات مباشرة لمتابعة أداء العمل", IsActive = true },
                    new Service { NameAr = "دعم الدفع الإلكتروني", NameEn = "Online Payments Support", Icon = "⚡", DescriptionAr = "ربط كامل مع بوابات الدفع الإلكتروني الآمنة", IsActive = true }
                );
                context.SaveChanges();
            }

            if (!context.Bookings.Any())
            {
                var firstServiceId = context.Services.FirstOrDefault()?.Id ?? 1;
                context.Bookings.AddRange(
                    new Booking
                    {
                        FullName = "أحمد محمود",
                        BusinessType = "شركة ركاز التقنية",
                        CountryCode = "+966",
                        Phone = "0501234567",
                        ServiceId = firstServiceId,
                        BookingDate = "2026-09-10",
                        SelectedSlot = "10:00 AM",
                        CreatedAt = DateTime.UtcNow.AddDays(-1)
                    },
                    new Booking
                    {
                        FullName = "أحمد محمود",
                        BusinessType = "شركة ركاز التقنية",
                        CountryCode = "+966",
                        Phone = "0501234567",
                        ServiceId = firstServiceId,
                        BookingDate = "2026-09-15",
                        SelectedSlot = "02:00 PM",
                        CreatedAt = DateTime.UtcNow.AddDays(-3)
                    },
                    new Booking
                    {
                        FullName = "سارة علي",
                        BusinessType = "عيادة الأمل",
                        CountryCode = "+966",
                        Phone = "0555555555",
                        ServiceId = firstServiceId,
                        BookingDate = "2026-09-12",
                        SelectedSlot = "11:30 AM",
                        CreatedAt = DateTime.UtcNow.AddDays(-5)
                    }
                );
                context.SaveChanges();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[DbInitializer Warning]: {ex.Message}");
        }
    }
}
