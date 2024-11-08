using AutoMapper;
using Microsoft.Extensions.Logging;
using WeatherAlertApp.Common.Models;
using WeatherAlertApp.DataAccess.DataAccess.Repositories.Abstractions;
using WeatherAlertApp.Services.Services.Abstractions;
using OneOf;
using WeatherAlertApp.Common.Helpers;
using WeatherAlertApp.Common.Models.ClientApiResponses;

namespace WeatherAlertApp.Services.Services;

public class WeatherForecastService : IWeatherForecastService
{
    private readonly IWeatherForecastRepository _weatherForecastRepository;
    private readonly IAccuWeatherApiClient _weatherApiClient;
    private readonly IMapper _mapper;
    private readonly ILogger<WeatherForecastService> _logger;

    private const string CityKeyJson = "Key"; 

    public WeatherForecastService(
        IWeatherForecastRepository weatherForecastRepository,
        IAccuWeatherApiClient weatherApiClient,
        IMapper mapper,
        ILogger<WeatherForecastService> logger)
    {
        _weatherForecastRepository = weatherForecastRepository;
        _weatherApiClient = weatherApiClient;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<OneOf<WeatherForecast, string>> GetByCity(string cityName) //TODO: improve city searching for cities with the identical names
    {
        var cityResponse = await _weatherApiClient.SearchCityByNameAsync(cityName);
        
        if (cityResponse.FastStartsWith("[]") || cityResponse.FastStartsWith("Error"))
        {
            var errorMessage = $"Error: Unable to find city with name: {cityName}.";
            _logger.Log(LogLevel.Information, errorMessage);
            
            return errorMessage;
        }

        var locationKey = ExtractLocationKey(cityResponse);
        
        if (string.IsNullOrEmpty(locationKey))
        {
            var errorMessage = $"Error: Invalid location key: {locationKey} for city: {cityName}.";
            _logger.Log(LogLevel.Information, errorMessage);
            
            return errorMessage;
        }

        var forecastId = int.Parse(locationKey);
        var forecastModel = await _weatherForecastRepository.GetTodayForecastByCityAsync(forecastId);
        
        if (forecastModel is null || forecastModel.Date != DateOnly.FromDateTime(DateTime.Today))
        {
            var forecastResponse = await _weatherApiClient.GetDailyForecastAsync(locationKey);
        
            if (forecastResponse.FastStartsWith("Error"))
            {
                var errorMessage = "Error: Unable to fetch forecast.";
                _logger.Log(LogLevel.Warning, errorMessage);
            
                return errorMessage;
            }

            var weatherApiResponse = forecastResponse.DeserializeWeatherApiResponse();
        
            if (weatherApiResponse == null)
            {
                var errorMessage = "Error: Failed to parse forecast response.";
                _logger.Log(LogLevel.Warning, errorMessage);
            
                return errorMessage;
            }

            forecastModel = _mapper.Map<WeatherForecast>(weatherApiResponse);

            forecastModel.Id = forecastId;
            forecastModel.City = cityName;
            forecastModel.Date = DateOnly.FromDateTime(DateTime.Today);
            
            await _weatherForecastRepository.AddOrUpdateAsync(forecastModel);
        }
        
        return forecastModel;
    }
    
    private string ExtractLocationKey(string cityResponse)
    {
        var locationKey = cityResponse.GetValueFromJson(CityKeyJson);
        
        return locationKey;
    }
}
