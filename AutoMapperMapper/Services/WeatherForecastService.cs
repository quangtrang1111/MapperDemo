﻿using MapperShared.Models;
using MapperShared.Services;

namespace AutoMapperMapper.Services
{
    public class WeatherForecastService : IWeatherForecastService
    {
        public WeatherForecastModel CreateWeatherForecast(WeatherForecastModel model)
        {
            return model;
        }
    }
}