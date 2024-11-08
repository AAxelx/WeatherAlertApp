using AutoMapper;
using WeatherAlertApp.Common.Entities;
using WeatherAlertApp.Common.Models;
using WeatherAlertApp.Common.Models.ClientApiResponses;

namespace WeatherAlertApp.Common.Helpers;

public class WeatherMappingProfile : Profile
{
    public WeatherMappingProfile()
    {
        CreateMap<WeatherApiResponse, WeatherForecast>()
            .ForMember(dest => dest.City, opt => opt.Ignore())
            .ForMember(dest => dest.MinTemperature,
                opt => opt.MapFrom(src => src.DailyForecasts.First().Temperature.Minimum.Value))
            .ForMember(dest => dest.MaxTemperature,
                opt => opt.MapFrom(src => src.DailyForecasts.First().Temperature.Maximum.Value))
            .ForMember(dest => dest.HasPrecipitation,
                opt => opt.MapFrom(src =>
                    src.DailyForecasts.First().Day.HasPrecipitation ||
                    src.DailyForecasts.First().Night.HasPrecipitation));

        CreateMap<WeatherForecast, WeatherForecastEntity>().ReverseMap();
    }
}