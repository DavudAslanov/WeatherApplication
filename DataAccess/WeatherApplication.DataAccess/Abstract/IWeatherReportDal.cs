using System;
using WeatherApplication.Core.DataAccess.Abstract;
using WeatherApplication.Entities.Concrete.DTOs;
using WeatherApplication.Entities.Concrete.TableModels;

namespace WeatherApplication.DataAccess.Abstract;

public interface IWeatherReportDal:IBaseInterface<WeatherReport>
{
    List<WeatherReportDto> GetWeatherReport();
}

