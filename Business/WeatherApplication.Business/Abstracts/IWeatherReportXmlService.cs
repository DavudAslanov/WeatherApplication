using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApplication.Core.Results.Abstract;
using WeatherApplication.Entities.Concrete.DTOs;
using WeatherApplication.Entities.Concrete.TableModels.ModelXml;
using WeatherApplication.Entities.Concrete.TableModels;

namespace WeatherApplication.Business.Abstracts
{
    public interface IWeatherReportXmlService
    {
        IDataResult<List<WeatherReportXml>> GetAllWeatherReportsAsync();
        IDataResult<WeatherReportXml> GetWeatherReportByIdAsync(int id);
        IResult SoftDeleteWeatherReport(int id);
        IResult HardDeleteWeatherReport(int id);
    }
}
