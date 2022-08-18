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
        private readonly IWeatherForecastService _service;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IWeatherForecastService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet(Name = "GetWeatherForecast")]
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

        [HttpPost()]
        public WeatherForecastDto Create([FromBody] CreateWeatherForecast request)
        {
            // Map one Model to a DTO
            var model = request.Adapt<WeatherForecastModel>();
            var created = _service.CreateWeatherForecast(model);

            // Map one property of a Model to a different property in the DTO
            var dto = created.Adapt<WeatherForecastDto>();

            return dto;
        }
    }
}