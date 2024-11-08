using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WeatherAlertApp.Common.Entities;
using WeatherAlertApp.Common.Models;
using WeatherAlertApp.DataAccess.DataAccess.Contexts;
using WeatherAlertApp.DataAccess.DataAccess.Repositories.Abstractions;

namespace WeatherAlertApp.DataAccess.DataAccess.Repositories;

public class WeatherForecastRepository : IWeatherForecastRepository
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public WeatherForecastRepository(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<WeatherForecast?> GetTodayForecastByCityAsync(int forecastId)
    {
        var forecast = await _context.WeatherForecasts.AsNoTracking()
            .FirstOrDefaultAsync(f => f.Id == forecastId && f.Date == DateOnly.FromDateTime(DateTime.Today));
        
        return _mapper.Map<WeatherForecast>(forecast);
    }

    public async Task AddOrUpdateAsync(WeatherForecast model)
    {
        var forecast = await _context.WeatherForecasts.FirstOrDefaultAsync(w => w.Id == model.Id);

        if (forecast is not null)
        {
            forecast.Date = model.Date;
            _context.Update(forecast);
        }
        else
        {
            var entity = _mapper.Map<WeatherForecastEntity>(model);
            await _context.AddAsync(entity);
        }
        
        await _context.SaveChangesAsync();
    }
}