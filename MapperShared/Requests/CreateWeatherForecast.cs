using System;
using System.Collections.Generic;
using System.Text;

namespace MapperShared.Requests
{
    public class CreateWeatherForecast
    {
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public string? Summary { get; set; }

        public int LocationId { get; set; }
    }
}
