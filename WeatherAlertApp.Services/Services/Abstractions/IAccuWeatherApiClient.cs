

namespace WeatherAlertApp.Services.Services.Abstractions;

public interface IAccuWeatherApiClient
{
    Task<string> SearchCityByNameAsync(string cityName,
        CancellationToken cancellationToken = default);
    Task<string> GetDailyForecastAsync(string locationKey,
        CancellationToken cancellationToken = default);
}
