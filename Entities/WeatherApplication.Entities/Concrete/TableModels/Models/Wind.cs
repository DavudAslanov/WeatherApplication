using System;
namespace WeatherApplication.Entities.Concrete.TableModels.Models
{
    public class Wind
    {
        public float Speed { get; set; }
        public float Deg { get; set; }
        public float? Gust { get; set; }
    }
}

