namespace Rekaz.Api.Infrastructure.Persistence;

using Microsoft.EntityFrameworkCore;
using Rekaz.Api.Core.Entities;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Service> Services => Set<Service>();
    public DbSet<Booking> Bookings => Set<Booking>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Service>(entity =>
        {
            entity.ToTable("Services");
            entity.HasKey(s => s.Id);

            entity.Property(s => s.NameAr).IsRequired().HasMaxLength(100);
            entity.Property(s => s.NameEn).IsRequired().HasMaxLength(100);
            entity.Property(s => s.Icon).HasMaxLength(50);
            entity.Property(s => s.DescriptionAr).HasMaxLength(500);
            entity.Property(s => s.IsActive).HasDefaultValue(true);
            entity.Property(s => s.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
        });

        modelBuilder.Entity<Booking>(entity =>
        {
            entity.ToTable("Bookings");
            entity.HasKey(b => b.Id);

            entity.Property(b => b.FullName).IsRequired().HasMaxLength(150);
            entity.Property(b => b.BusinessType).IsRequired().HasMaxLength(100);
            entity.Property(b => b.CountryCode).IsRequired().HasMaxLength(10);
            entity.Property(b => b.Phone).IsRequired().HasMaxLength(20);
            entity.Property(b => b.BookingDate).IsRequired().HasMaxLength(20);
            entity.Property(b => b.SelectedSlot).IsRequired().HasMaxLength(20);
            entity.Property(b => b.CreatedAt).HasDefaultValueSql("GETUTCDATE()");

            entity.HasOne(b => b.Service)
                .WithMany()
                .HasForeignKey(b => b.ServiceId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}
