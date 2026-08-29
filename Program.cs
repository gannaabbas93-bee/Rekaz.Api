var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddOpenApi();

// Enable CORS for Angular frontend & Vercel deployment
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular", policy =>
    {
        policy.SetIsOriginAllowed(_ => true)
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors("AllowAngular");

// GET /api/home endpoint
app.MapGet("/api/home", () =>
{
    var response = new HomeResponse(
        TitleAr: "أهلاً بك في ركاز",
        TitleEn: "Welcome to Rekaz",
        MessageAr: "منصة إدارة الحجوزات والاشتراكات الذكية التي تمنح أعمالك الكفاءة والنمو",
        MessageEn: "Smart Bookings & Memberships Management Platform driving growth and efficiency",
        Services: new[]
        {
            new ServiceItem(1, "إدارة الحجوزات", "Bookings Management", "📅", "حجز وإدارة الجلسات والخدمات بسهولة وسلاسة"),
            new ServiceItem(2, "إدارة الاشتراكات", "Memberships Management", "💳", "تتبع خطط الاشتراكات والعضويات والتجديد التلقائي"),
            new ServiceItem(3, "تقارير وأداء", "Reports & Analytics", "📊", "تحليلات دقيقة وإحصائيات مباشرة لمتابعة أداء العمل"),
            new ServiceItem(4, "دعم الدفع الإلكتروني", "Online Payments Support", "⚡", "ربط كامل مع بوابات الدفع الإلكتروني الآمنة")
        },
        ServerTime: DateTime.Now,
        BackendVersion: "NET 10.0 API"
    );

    return Results.Ok(response);
})
.WithName("GetHomeData");

// GET /api/availability endpoint
app.MapGet("/api/availability", (int? serviceId, string? date) =>
{
    var sId = serviceId ?? 1;
    var targetDate = string.IsNullOrWhiteSpace(date) ? DateTime.Today.ToString("yyyy-MM-dd") : date;

    var slots = sId switch
    {
        2 => new[] { "09:00 AM", "11:30 AM", "02:00 PM", "04:30 PM", "07:00 PM" },
        3 => new[] { "10:30 AM", "01:00 PM", "03:30 PM", "06:00 PM" },
        4 => new[] { "09:30 AM", "12:00 PM", "02:30 PM", "05:00 PM", "08:30 PM" },
        _ => new[] { "10:00 AM", "12:00 PM", "03:00 PM", "05:30 PM", "08:00 PM" }
    };

    var response = new AvailabilityResponse(
        ServiceId: sId,
        Date: targetDate,
        AvailableSlots: slots,
        MessageAr: $"المواعيد المتاحة بتاريخ {targetDate}",
        MessageEn: $"Available slots on {targetDate}",
        ServerTime: DateTime.Now
    );

    return Results.Ok(response);
})
.WithName("GetAvailability");

app.Run();

public record HomeResponse(
    string TitleAr,
    string TitleEn,
    string MessageAr,
    string MessageEn,
    ServiceItem[] Services,
    DateTime ServerTime,
    string BackendVersion
);

public record ServiceItem(
    int Id,
    string NameAr,
    string NameEn,
    string Icon,
    string DescriptionAr
);

public record AvailabilityResponse(
    int ServiceId,
    string Date,
    string[] AvailableSlots,
    string MessageAr,
    string MessageEn,
    DateTime ServerTime
);


