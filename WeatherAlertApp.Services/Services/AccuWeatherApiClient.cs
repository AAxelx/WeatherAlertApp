using Microsoft.Extensions.Options;
using WeatherAlertApp.Common.Configurations;
using WeatherAlertApp.Services.Services.Abstractions;
using OneOf;

namespace WeatherAlertApp.Services.Services;

public class AccuWeatherApiClient : IAccuWeatherApiClient
{
    private readonly HttpClient _httpClient;
    private readonly WeatherApiOptions _options;
    
    public AccuWeatherApiClient(HttpClient httpClient, IOptions<WeatherApiOptions> options)
    {
        _httpClient = httpClient;
        _options = options.Value;
    }
    
    public async Task<string> SearchCityByNameAsync(string cityName, CancellationToken cancellationToken = default)
    {
        var requestUrl = $"{_options.BaseUrl}{_options.SearchCityEndpoint}?apikey={_options.ApiKey}&q={cityName}";

        var response = await _httpClient.GetAsync(requestUrl, cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            return $"Error: {response.StatusCode} - {response.ReasonPhrase}";
        }
        
        var content = await response.Content.ReadAsStringAsync(cancellationToken);
        return content;
    }
    
    public async Task<string> GetDailyForecastAsync(string locationKey, CancellationToken cancellationToken = default)
    {
        var requestUrl = $"{_options.BaseUrl}{_options.ForecastEndpoint}{locationKey}?apikey={_options.ApiKey}";

        var response = await _httpClient.GetAsync(requestUrl, cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            return $"Error: {response.StatusCode} - {response.ReasonPhrase}";
        }
        
        var content = await response.Content.ReadAsStringAsync(cancellationToken);
        return content;
    }
}