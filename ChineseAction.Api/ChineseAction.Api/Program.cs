using Microsoft.EntityFrameworkCore;
using ChineseAction.Api.Data;
using ChineseAction.Api.Repository;
using ChineseAction.Api.Servies;

var builder = WebApplication.CreateBuilder(args);

// DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// רישום שכבות - Scoped (מתאים ל-DbContext)
builder.Services.AddScoped<IGiftRepository, GiftRepository>();
builder.Services.AddScoped<IGiftService, GiftService>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IDonorRepository, DonorRepository>();
builder.Services.AddScoped<IDonorService, DonorService>();
builder.Services.AddScoped<IPurchaserRepository, PurchaserRepository>();
builder.Services.AddScoped<IPurchaserService, PurchaserService>();
// הוספת שורות אלה ל-Program.cs (מיקום בתוך builder.Services registration)
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();
// Swagger / OpenAPI
builder.Services.AddEndpointsApiExplorer(); 
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();
var app = builder.Build();

// Swagger middleware
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "V1");
    c.RoutePrefix = string.Empty; // הופך את Swagger UI לזמין בכתובת הבסיסית (/)
});

app.MapControllers();
app.Run();