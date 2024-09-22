using System;
using System.Linq.Expressions;
using WeatherApplication.Core.DataAccess.Abstract;
using WeatherApplication.Core.DataAccess.Concrete;
using WeatherApplication.DataAccess.Abstract;
using WeatherApplication.DataAccess.Context;
using WeatherApplication.Entities.Concrete.TableModels;

namespace WeatherApplication.DataAccess.Concrete;
public class DistrictDal : BaseRepository<District, AppDbContext>, IDistrictDal
{
}