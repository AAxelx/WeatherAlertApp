namespace WeatherAlertApp.Common.Models.ClientApiResponses;

public class WeatherApiResponse
{
    public Headline Headline { get; set; }
    public List<DailyForecast> DailyForecasts { get; set; }
}

public class Headline
{
    public DateTime EffectiveDate { get; set; }
    public long EffectiveEpochDate { get; set; }
    public int Severity { get; set; }
    public string Text { get; set; }
    public string Category { get; set; }
    public DateTime? EndDate { get; set; }
    public long? EndEpochDate { get; set; }
    public string MobileLink { get; set; }
    public string Link { get; set; }
}

public class DailyForecast
{
    public DateTime Date { get; set; }
    public long EpochDate { get; set; }
    public Temperature Temperature { get; set; }
    public DayNightData Day { get; set; }
    public DayNightData Night { get; set; }
    public List<string> Sources { get; set; }
    public string MobileLink { get; set; }
    public string Link { get; set; }
}

public class Temperature
{
    public TemperatureValue Minimum { get; set; }
    public TemperatureValue Maximum { get; set; }
}

public class TemperatureValue
{
    public decimal Value { get; set; }
    public string Unit { get; set; }
    public int UnitType { get; set; }
}

public class DayNightData
{
    public int Icon { get; set; }
    public string IconPhrase { get; set; }
    public bool HasPrecipitation { get; set; }
}
