using AutoMapper;
using MapperShared.DTOs;
using MapperShared.Models;
using MapperShared.Requests;

namespace AutoMapperMapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<LocationModel, LocationDto>();
            CreateMap<CreateWeatherForecast, WeatherForecastModel>();
            CreateMap<WeatherForecastModel, WeatherForecastDto>()
                .ForMember(dest => dest.TemperatureF, opt => opt.MapFrom(src => src.TemperatureC));
        }
    }
}
