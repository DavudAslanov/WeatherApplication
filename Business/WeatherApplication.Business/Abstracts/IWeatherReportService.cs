using System;
using WeatherApplication.Core.Results.Abstract;
using WeatherApplication.Entities.Concrete.DTOs;
using WeatherApplication.Entities.Concrete.TableModels;

namespace WeatherApplication.Business.Abstracts
{
    public interface IWeatherReportService
    {
        IDataResult<List<WeatherReportDto>> GetAllAsync();
        IDataResult<WeatherReport> GetByIdAsync(int id);
        IResult SoftDelete(int id);
        IResult HardDelete(int id);
    }
}

