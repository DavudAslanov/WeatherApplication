using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WeatherApplication.Entities.Concrete.TableModels.ModelXml
{
    public class DistrictXml
    {
        public DistrictXml()
        {
            WeatherReports = new List<WeatherReportXml>();
        }

        [XmlElement("Id")]
        public int Id { get; set; }

        [XmlElement("Title")]
        public string Title { get; set; }

        [XmlElement("Latitude")]
        public double Latitude { get; set; }

        [XmlElement("Longitude")]
        public double Longitude { get; set; }

        [XmlArray("WeatherReports")]
        [XmlArrayItem("WeatherReport")]
        public List<WeatherReportXml> WeatherReports { get; set; }
    }
}
