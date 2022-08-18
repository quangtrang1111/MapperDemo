using AutoMapper;
using MapperShared.DTOs;
using MapperShared.Models;
using MapperShared.Requests;
using MapperShared.Services;
using Microsoft.AspNetCore.Mvc;

namespace AutoMapperMapper.Controllers
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
        private readonly IMapper _mapper;
        private readonly ILocationService _locationService;

        public WeatherForecastController(
            ILogger<WeatherForecastController> logger,
            IMapper mapper,
            ILocationService service)
        {
            _logger = logger;
            _locationService = service;
            _mapper = mapper;
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
            var model = _mapper.Map<WeatherForecastModel>(request);

            // Map one property of a Model to a different property in the DTO
            var dto = _mapper.Map<WeatherForecastDto>(model);

            return dto;
        }

        [HttpPost("CreateWithLocation")]
        public async Task<WeatherForecastDto> CreateWithLocation([FromBody] CreateWeatherForecast request)
        {
            // Map one Model to a DTO
            var model = _mapper.Map<WeatherForecastModel>(request);

            var location = await _locationService.GetByIdAsync(model.LocationId);

            // Map one property of a Model to a different property in the DTO
            var dto = _mapper.Map<WeatherForecastModel, WeatherForecastDto>(model, opt =>
            {
                opt.AfterMap((WeatherForecastModel src, WeatherForecastDto dest) =>
                {
                    dest.Location = _mapper.Map<LocationDto>(location);
                });
            });

            return dto;
        }
    }
}