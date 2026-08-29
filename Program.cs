var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddOpenApi();

// Enable CORS for Angular frontend
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular", policy =>
    {
        policy.WithOrigins("http://localhost:4200", "https://localhost:4200")
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

