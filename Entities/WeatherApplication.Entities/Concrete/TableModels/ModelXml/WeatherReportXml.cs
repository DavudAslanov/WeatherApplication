using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WeatherApplication.Entities.Concrete.TableModels.ModelXml
{
    [XmlRoot("WeatherReport")]
    public class WeatherReportXml
    {
        [Key]
        [XmlElement("Id")]
        public int ID { get; set; }
        
        [XmlElement("WeatherId")]
        public int WeatherId { get; set; }

        [XmlElement("Main")]
        public string? Main { get; set; }

        [XmlElement("Description")]
        public string? Description { get; set; }

        [XmlElement("Icon")]
        public string? Icon { get; set; }

        [XmlElement("Temp")]
        public float Temp { get; set; } 

        [XmlElement("FeelsLike")]
        public float FeelsLike { get; set; }

        [XmlElement("TempMin")]
        public float TempMin { get; set; }

        [XmlElement("TempMax")]
        public float TempMax { get; set; }

        [XmlElement("Pressure")]
        public float Pressure { get; set; }

        [XmlElement("Humidity")]
        public float Humidity { get; set; }

        [XmlElement("SeaLevel")]
        public float SeaLevel { get; set; } = 0;

        [XmlElement("GroundLevel")]
        public float GroundLevel { get; set; } = 0;

        [XmlElement("WindSpeed")]
        public float WindSpeed { get; set; }

        [XmlElement("WindDegree")]
        public float WindDegree { get; set; }

        [XmlElement("WindGust")]
        public float WindGust { get; set; } = 0;

        [XmlElement("Clouds")]
        public float Clouds { get; set; }

        [XmlElement("DateTime")]
        public DateTime DateTime { get; set; }

        [XmlElement("DistrictId")]
        public int DistrictId { get; set; }

        [XmlElement("IsDeleted")]
        public int IsDeleted { get; set; }=0;

        [XmlElement("UpdateDate")]
        public DateTime ?UpdateDate { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string XmlData { get; set; }

        [XmlIgnore]
        public District District { get; set; }
       
    }
}
