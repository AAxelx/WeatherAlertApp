using System.Text.Json;
using WeatherAlertApp.Common.Models.ClientApiResponses;

namespace WeatherAlertApp.Common.Helpers;

public static class JsonExtensions
{
    public static string GetValueFromJson(this string jsonString, string key)
    {
        var doc = JsonDocument.Parse(jsonString);

        var rootElement = doc.RootElement[0];

        if (rootElement.TryGetProperty(key, out JsonElement property))
        {
            return property.GetString();
        }

        return string.Empty;
    }

    public static WeatherApiResponse? DeserializeWeatherApiResponse(this string forecastResponse)
    {
        if (string.IsNullOrWhiteSpace(forecastResponse))
        {
            return null;
        }
        
        var result = JsonSerializer.Deserialize<WeatherApiResponse>(forecastResponse);

        return result;
    }
}