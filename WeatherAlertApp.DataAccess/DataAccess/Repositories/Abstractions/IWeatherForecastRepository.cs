using WeatherAlertApp.Common.Models;

namespace WeatherAlertApp.DataAccess.DataAccess.Repositories.Abstractions;

public interface IWeatherForecastRepository
{
    Task<WeatherForecast?> GetTodayForecastByCityAsync(int id);
    Task AddOrUpdateAsync(WeatherForecast model);
}