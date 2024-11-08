using System.ComponentModel.DataAnnotations.Schema;

namespace WeatherAlertApp.Common.Entities;

public class WeatherForecastEntity
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int Id { get; set; }
    public string City { get; set; }
    public DateOnly Date { get; set; }
    public int MinTemperature { get; set; }
    public int MaxTemperature { get; set; }
    public bool HasPrecipitation { get; set; }
}