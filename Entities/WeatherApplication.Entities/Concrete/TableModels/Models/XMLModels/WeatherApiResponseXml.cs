using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WeatherApplication.Entities.Concrete.TableModels.Models.XMLModels
{
    [XmlRoot("WeatherApiResponse")]
    public class WeatherApiResponseXml
    {
        [XmlElement("Weather")]
        public List<Weather> Weather { get; set; }

        [XmlElement("Main")]
        public Main Main { get; set; }

        [XmlElement("Wind")]
        public Wind Wind { get; set; }

        [XmlElement("Clouds")]
        public Clouds Clouds { get; set; }
    }
}
