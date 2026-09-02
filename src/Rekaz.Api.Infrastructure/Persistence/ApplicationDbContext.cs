namespace Rekaz.Api.Infrastructure.Persistence;

using Microsoft.EntityFrameworkCore;
using Rekaz.Api.Core.Entities;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<Service> Services => Set<Service>();
    public DbSet<Booking> Bookings => Set<Booking>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // User Configuration
        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("Users");
            entity.HasKey(u => u.Id);
            entity.Property(u => u.FullName).IsRequired().HasMaxLength(150);
            entity.Property(u => u.Email).IsRequired().HasMaxLength(150);
            entity.HasIndex(u => u.Email).IsUnique();
            entity.Property(u => u.PasswordHash).IsRequired();
            entity.Property(u => u.Role).HasConversion<string>().HasMaxLength(50);
            entity.Property(u => u.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
        });

        // Service Configuration
        modelBuilder.Entity<Service>(entity =>
        {
            entity.ToTable("Services");
            entity.HasKey(s => s.Id);
            entity.Property(s => s.NameAr).IsRequired().HasMaxLength(100);
            entity.Property(s => s.NameEn).IsRequired().HasMaxLength(100);
            entity.Property(s => s.Icon).HasMaxLength(50).HasDefaultValue("");
            entity.Property(s => s.DescriptionAr).HasMaxLength(500).HasDefaultValue("");
            entity.Property(s => s.IsActive).HasDefaultValue(true);
            entity.Property(s => s.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
        });

        // Booking Configuration
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
                  .WithMany(s => s.Bookings)
                  .HasForeignKey(b => b.ServiceId)
                  .OnDelete(DeleteBehavior.Restrict);
        });
    }
}
