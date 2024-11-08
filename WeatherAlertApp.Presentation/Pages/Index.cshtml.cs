using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WeatherAlertApp.Common.Models;
using WeatherAlertApp.Services.Services.Abstractions;

namespace WeatherAlertApp.Presentation.Pages;

public class IndexModel : PageModel
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IWeatherForecastService _weatherForecastService;

    [BindProperty] 
    public WeatherForecast Weather { get; set; } = new ();
    [BindProperty]
    public string City { get; set; }
    
    public bool IsFirstAlert { get; set; }
    public string Error { get; set; }


    public IndexModel(
        IHttpContextAccessor httpContextAccessor,
        IWeatherForecastService weatherForecastService)
    {
        _httpContextAccessor = httpContextAccessor;
        _weatherForecastService = weatherForecastService;
    }

    public async Task OnGet()
    {
        City = _httpContextAccessor.HttpContext.Session.GetString("LastCity");

        if (City != null)
        {
            await GetWeatherDataAsync(City);
            HandleRainAlert(City);
        }
    }

    public async Task OnPost()
    {
        _httpContextAccessor.HttpContext.Session.SetString("LastCity", City);
        await GetWeatherDataAsync(City);
        HandleRainAlert(City);
    }

    private async Task GetWeatherDataAsync(string city)
    {
        var response = await _weatherForecastService.GetByCity(city);
        response.Switch(
            success => Weather = success,
            failed => Error = failed);
    }

    private void HandleRainAlert(string city)
    {
        var rainAlertKey = $"RainAlert_{city}_{DateTime.Today.ToShortDateString()}";
        var alert = _httpContextAccessor.HttpContext.Session.GetString(rainAlertKey);

        if (Weather.HasPrecipitation && string.IsNullOrEmpty(alert))
        {
            _httpContextAccessor.HttpContext.Session.SetString(rainAlertKey, "True");
            IsFirstAlert = true;
        }
        else
        {
            IsFirstAlert = false;
        }
    }
}