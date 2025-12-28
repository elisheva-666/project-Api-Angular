using ChineseAction.Api.Data;
using ChineseAction.Api.Repository;
using ChineseAction.Api.Servies; // שים לב שתוקנה כאן שגיאת הכתיב שהייתה קודם
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using Microsoft.OpenApi.Models; // זה ה-Using הקריטי ל-Swagger
using Serilog;
using System.Text;
using System.Text.Json.Serialization;
using System.IO;


var builder = WebApplication.CreateBuilder(args);

// --- התחלת הגדרת הלוגים ---
// הגדרה: לכתוב גם לקונסול (מסך שחור) וגם לקובץ טקסט
string logPath = Path.Combine(builder.Environment.ContentRootPath, "logs/log-.txt");

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console()
    .WriteTo.File(logPath, rollingInterval: RollingInterval.Day) // כאן אנחנו משתמשים בנתיב החדש
    .CreateLogger();

builder.Host.UseSerilog();

// 1. חיבור ל-DB
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 2. רישום שירותים (Services & Repositories)
builder.Services.AddScoped<IGiftRepository, GiftRepository>();
builder.Services.AddScoped<IGiftService, GiftService>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IDonorRepository, DonorRepository>();
builder.Services.AddScoped<IDonorService, DonorService>();
builder.Services.AddScoped<IPurchaserRepository, PurchaserRepository>();
builder.Services.AddScoped<IPurchaserService, PurchaserService>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();

// 3. הגדרת Swagger עם כפתור מנעול (Authorize)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ChineseSaleAPI", Version = "v1" });

    // הגדרת הסכמה (Scheme) לאבטחה - הוספת כפתור "Authorize"
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme.\r\n\r\nEnter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,
            },
            new List<string>()
        }
    });
});

// 4. הוספת שירותי אימות (JWT Authentication)
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

// 5. הוספת קונטרולרים עם תיקון למעגליות ב-JSON
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

var app = builder.Build();

// 6. הגדרת Pipeline (Middleware)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "ChineseSaleAPI V1");
        c.RoutePrefix = string.Empty; // הופך את Swagger לדף הבית

    });
}

app.UseHttpsRedirection();

// סדר קריטי: קודם Authentication ואז Authorization
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();