using System;
using WeatherApplication.Core.DataAccess.Concrete;
using WeatherApplication.DataAccess.Abstract;
using WeatherApplication.DataAccess.Context;
using WeatherApplication.Entities.Concrete.DTOs;
using WeatherApplication.Entities.Concrete.TableModels;

namespace WeatherApplication.DataAccess.Concrete
{
    public class WeatherReportDal : BaseRepository<WeatherReport, AppDbContext>, IWeatherReportDal
    {
        private readonly AppDbContext _context;
        public WeatherReportDal(AppDbContext context)
        {
            _context = context;
        }
        public List<WeatherReportDto> GetWeatherReport()
        {
            var result = from weatherReport in _context.WeatherReports
                         where weatherReport.IsDeleted == 0
                         join discrit in _context.Districts
                         on weatherReport.DistrictId equals discrit.Id
                         where discrit.IsDeleted == 0
                         select new WeatherReportDto
                         {
                             Id = weatherReport.Id,
                             WeatherId = weatherReport.WeatherId,
                             Main = weatherReport.Main,
                             Description = weatherReport.Description,
                             Icon = weatherReport.Icon,
                             Temp = weatherReport.Temp,
                             FeelsLike = weatherReport.FeelsLike,
                             TempMin = weatherReport.TempMin,
                             TempMax = weatherReport.TempMax,
                             Pressure = weatherReport.Pressure,
                             Humidity = weatherReport.Humidity,
                             SeaLevel = weatherReport.SeaLevel,
                             GroundLevel = weatherReport.GroundLevel,
                             WindDegree = weatherReport.WindDegree,
                             WindGust = weatherReport.WindGust,
                             Clouds = weatherReport.Clouds,
                             DateTime = weatherReport.DateTime,
                             DistrictId = weatherReport.DistrictId,
                             WindSpeed = weatherReport.WindSpeed,
                             WeatherDiscritName = discrit.Title
                         };
            return result.ToList();

        }
    }
}

