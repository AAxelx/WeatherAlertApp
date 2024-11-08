using Microsoft.EntityFrameworkCore;
using WeatherAlertApp.Common.Configurations;
using WeatherAlertApp.Common.Helpers;
using WeatherAlertApp.DataAccess.DataAccess.Contexts;
using WeatherAlertApp.DataAccess.DataAccess.Repositories;
using WeatherAlertApp.DataAccess.DataAccess.Repositories.Abstractions;
using WeatherAlertApp.Services.Services;
using WeatherAlertApp.Services.Services.Abstractions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpClient();
builder.Services.AddHttpClient<IAccuWeatherApiClient, AccuWeatherApiClient>()
    .ConfigureHttpClient(client => client.Timeout = TimeSpan.FromSeconds(30)); // Настройка тайм-аута

builder.Services.AddRazorPages();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
});

// Регистрируем IHttpContextAccessor
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.Configure<WeatherApiOptions>(
    builder.Configuration.GetSection("AccuWeather"));

builder.Services.AddAutoMapper(typeof(WeatherMappingProfile));

builder.Services.AddScoped<IWeatherForecastService, WeatherForecastService>();

builder.Services.AddScoped<IWeatherForecastRepository, WeatherForecastRepository>();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.MapRazorPages();

app.Run();