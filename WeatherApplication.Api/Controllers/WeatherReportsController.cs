using Microsoft.AspNetCore.Mvc;
using WeatherApplication.Business.Abstracts;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WeatherApplication.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherReportsController : ControllerBase
    {
        private readonly IWeatherReportService _weatherReportService;
        public WeatherReportsController(IWeatherReportService weatherReportService)
        {
            _weatherReportService = weatherReportService;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _weatherReportService.GetAllAsync();
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("id")]
        public IActionResult GetById(int id)
        {
            var result = _weatherReportService.GetByIdAsync(id);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpDelete]
        public IActionResult SoftDelete(int id)
        {
            var result = _weatherReportService.SoftDelete(id);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpDelete("HardDelete")]
        public IActionResult HardDeleteDistrict(int id)
        {
            var result = _weatherReportService.HardDelete(id);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}

