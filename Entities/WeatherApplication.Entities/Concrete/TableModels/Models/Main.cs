using System;
namespace WeatherApplication.Entities.Concrete.TableModels.Models
{
    public class Main
    {
        public float Temp { get; set; }
        public float FeelsLike { get; set; }
        public float TempMin { get; set; }
        public float TempMax { get; set; }
        public float Pressure { get; set; }
        public float Humidity { get; set; }
        public float? SeaLevel { get; set; }
        public float? GrndLevel { get; set; }
    }
}

