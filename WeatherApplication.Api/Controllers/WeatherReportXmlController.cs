using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Xml.Serialization;
using WeatherApplication.Business.Abstracts;
using WeatherApplication.Entities.Concrete.TableModels.ModelXml;

namespace WeatherApplication.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherReportXmlController : ControllerBase
    {
        private readonly IWeatherReportXmlService _weatherReportXmlService;

        public WeatherReportXmlController(IWeatherReportXmlService weatherReportXmlService)
        {
            _weatherReportXmlService = weatherReportXmlService;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllWeatherReportsAsync()
        {
            var result =  _weatherReportXmlService.GetAllWeatherReportsAsync();
            if (result.IsSuccess)
            {
                var xmlSerializer = new XmlSerializer(typeof(List<WeatherReportXml>));
                using var stringWriter = new StringWriter();
                xmlSerializer.Serialize(stringWriter, result.Data);
                var xmlString = stringWriter.ToString();
                return Content(xmlString, "application/xml");
            }
            return NotFound(result.Message);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetWeatherReportByIdAsync(int id)
        {
            var result = _weatherReportXmlService.GetWeatherReportByIdAsync(id);
            if (result.IsSuccess)
            {
                var xmlSerializer = new XmlSerializer(typeof(WeatherReportXml));
                using var stringWriter = new StringWriter();
                xmlSerializer.Serialize(stringWriter, result.Data);
                var xmlString = stringWriter.ToString();
                return Content(xmlString, "application/xml");
            }
            return NotFound(result.Message);
        }

        [HttpDelete]
        public IActionResult SoftDelete(int id)
        {
            var result = _weatherReportXmlService.SoftDeleteWeatherReport(id);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpDelete("HardDelete")]
        public IActionResult HardDeleteDistrict(int id)
        {
            var result = _weatherReportXmlService.HardDeleteWeatherReport(id);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("download-xml")]
        public IActionResult DownloadXml()
        {
            var weatherDataResult = _weatherReportXmlService.GetAllWeatherReportsAsync(); // Veriyi çekiyoruz

            if (weatherDataResult.IsSuccess) // Sonuç başarılı mı kontrol ediyoruz
            {
                var weatherData = weatherDataResult.Data; // Asıl veriyi alıyoruz

                var xmlSerializer = new XmlSerializer(typeof(List<WeatherReportXml>));
                using (var stringWriter = new StringWriter())
                {
                    xmlSerializer.Serialize(stringWriter, weatherData);
                    var xmlString = stringWriter.ToString();
                    var byteArray = Encoding.UTF8.GetBytes(xmlString);
                    return File(byteArray, "application/xml", "weather_data.xml");
                }
            }
            else
            {
                return BadRequest("Veri getirilemedi.");
            }
        }


    }
}
