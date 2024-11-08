namespace WeatherAlertApp.Common.Models;

public class WeatherForecast
{
    public int Id { get; set; }
    public string City { get; set; }
    public int MinTemperature { get; set; }
    public int MaxTemperature { get; set; }
    public bool HasPrecipitation { get; set; }
    public DateOnly Date { get; set; }
}