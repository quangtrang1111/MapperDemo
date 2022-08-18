using MapperShared.Models;

namespace MapperShared.Services
{
    public interface IWeatherForecastService
    {
        WeatherForecastModel CreateWeatherForecast(WeatherForecastModel model);
    }
}
