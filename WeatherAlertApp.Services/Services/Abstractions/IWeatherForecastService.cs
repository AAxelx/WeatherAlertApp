using WeatherAlertApp.Common.Models;
using OneOf;

namespace WeatherAlertApp.Services.Services.Abstractions;

public interface IWeatherForecastService
{
    Task<OneOf<WeatherForecast, string>> GetByCity(string cityName);
}