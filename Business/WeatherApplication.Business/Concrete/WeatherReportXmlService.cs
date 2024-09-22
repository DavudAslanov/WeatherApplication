using Microsoft.Extensions.Logging;
using WeatherApplication.Business.Abstracts;
using WeatherApplication.Core.Results.Abstract;
using WeatherApplication.Core.Results.Concrete;
using WeatherApplication.DataAccess.Context;
using WeatherApplication.Entities.Concrete.TableModels.ModelXml;

namespace WeatherApplication.Business.Concrete
{
    public class WeatherReportXmlService : IWeatherReportXmlService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<WeatherReportXmlService> _logger;

        public WeatherReportXmlService(AppDbContext context, ILogger<WeatherReportXmlService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IDataResult<List<WeatherReportXml>> GetAllWeatherReportsAsync()
        {
            var reports = _context.WeatherReportXmls.ToList();
            return new SuccessDataResult<List<WeatherReportXml>>(reports);
        }

        public IDataResult<WeatherReportXml> GetWeatherReportByIdAsync(int id)
        {
            var report = _context.WeatherReportXmls.Find(id);
            if (report != null)
            {
                return new SuccessDataResult<WeatherReportXml>(report);
            }
            return new ErrorDataResult<WeatherReportXml>(default, "Report not found", false);
        }

        public IResult HardDeleteWeatherReport(int id)
        {
            var report = _context.WeatherReportXmls.Find(id);
            if (report != null)
            {
                _context.WeatherReportXmls.Remove(report);
                _context.SaveChanges();
                return new SuccessResult("Report soft deleted successfully.");
            }
            return new ErrorResult("Report not found.");
        }

        public IResult SoftDeleteWeatherReport(int id)
        {
            var report = _context.WeatherReportXmls.Find(id);
            if (report != null)
            {
                report.IsDeleted = id;
                _context.WeatherReportXmls.Update(report);
                _context.SaveChanges();
                return new SuccessResult("Report soft deleted successfully.");
            }
            return new ErrorResult("Report not found.");
        }
    }
}
