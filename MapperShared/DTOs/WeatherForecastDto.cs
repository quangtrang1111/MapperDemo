using System;
using System.Collections.Generic;
using System.Text;

namespace MapperShared.DTOs
{
    public class WeatherForecastDto
    {
        public DateTime Date { get; set; }

        public int TemperatureF { get; set; }

        public string? Summary { get; set; }
    }
}
