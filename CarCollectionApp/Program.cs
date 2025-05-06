using CarCollectionApp.Models;
using Microsoft.EntityFrameworkCore;
using CarCollectionApp.Services;
using CarCollectionApp.Repositories; 

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<CarCollectionContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllersWithViews();
builder.Services.AddScoped<CarCollectionApp.Services.ICarStatsService, CarCollectionApp.Services.CarStatsService>();

builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
builder.Services.AddScoped<IReservationService, ReservationService>();
builder.Services.AddScoped<IBrandService, BrandService>();
builder.Services.AddScoped<IDealerService, DealerService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.UseStaticFiles();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
