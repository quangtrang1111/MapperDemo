using MapperShared.DTOs;
using MapperShared.Models;
using MapperShared.Requests;
using MapperShared.Services;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace MapsterMapper.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly ILocationService _locationService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, ILocationService service)
        {
            _logger = logger;
            _locationService = service;
        }

        [HttpGet("GetWeatherForecast")]
        public IEnumerable<WeatherForecastModel> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecastModel
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpPost("Create")]
        public WeatherForecastDto Create([FromBody] CreateWeatherForecast request)
        {
            // Map one Model to a DTO
            var model = request.Adapt<WeatherForecastModel>();

            // Map one property of a Model to a different property in the DTO
            var dto = model.Adapt<WeatherForecastDto>();

            return dto;
        }

        [HttpPost("CreateWithLocation")]
        public async Task<WeatherForecastDto> CreateWithLocation([FromBody] CreateWeatherForecast request)
        {
            // Map one Model to a DTO
            var model = request.Adapt<WeatherForecastModel>();

            var location = await _locationService.GetByIdAsync(model.LocationId);

            // Map one property of a Model to a different property in the DTO
            var config = new TypeAdapterConfig();
            config.NewConfig<WeatherForecastModel, WeatherForecastDto>()
                .Map(dest => dest.TemperatureF, src => src.TemperatureC)
                .Map(dest => dest.Location, src => location.Adapt<LocationDto>());

            var dto = model.Adapt<WeatherForecastDto>(config);

            return dto;
        }
    }
}