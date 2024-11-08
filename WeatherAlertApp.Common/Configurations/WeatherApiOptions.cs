namespace WeatherAlertApp.Common.Configurations;

public class WeatherApiOptions
{
    public string ApiKey { get; set; }
    public string BaseUrl { get; set; }
    public string ForecastEndpoint { get; set; }
    public string SearchCityEndpoint { get; set; }
}
