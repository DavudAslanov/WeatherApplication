using System;
namespace WeatherApplication.Entities.Concrete.TableModels.Models
{
    public class WeatherApiResponse
    {
        public List<Weather> Weather { get; set; }
        public Main Main { get; set; }
        public Wind Wind { get; set; }
        public Clouds Clouds { get; set; }
    }
}

