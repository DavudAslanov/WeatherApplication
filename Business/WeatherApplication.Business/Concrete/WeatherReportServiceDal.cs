using WeatherApplication.Business.Abstracts;
using WeatherApplication.Business.BaseMessages;
using WeatherApplication.Core.Results.Abstract;
using WeatherApplication.Core.Results.Concrete;
using WeatherApplication.DataAccess.Abstract;
using WeatherApplication.Entities.Concrete.DTOs;
using WeatherApplication.Entities.Concrete.TableModels;

namespace WeatherApplication.Business.Concrete
{
    public class WeatherReportServiceDal : IWeatherReportService
    {
        private readonly IWeatherReportDal _weatherReportDal;
        public WeatherReportServiceDal(IWeatherReportDal weatherReportDal)
        {
            _weatherReportDal = weatherReportDal;
        }

        public IResult SoftDelete(int id)
        {
            var model = GetByIdAsync(id).Data;
            model.IsDeleted = id;
            _weatherReportDal.UpdateAsync(model);
            return new SuccessResult(UIMessage.DELETED_MESSAGE);
        }

        public IResult HardDelete(int id)
        {
            var model = GetByIdAsync(id).Data;
            if (model != null)
            {
                _weatherReportDal.DeleteAsync(model);
                return new SuccessResult(UIMessage.HARD_DELETED_MESSAGE);
            }
            else
            {
                return new SuccessResult(UIMessage.NOT_DELETED_MESSAGE);
            }
            
            
        }

        public IDataResult<List<WeatherReportDto>> GetAllAsync()
        {
            return new SuccessDataResult<List<WeatherReportDto>>(_weatherReportDal.GetWeatherReport());
        }

        public IDataResult<WeatherReport> GetByIdAsync(int id)
        {
            var result = _weatherReportDal.GetById(id);
            return new SuccessDataResult<WeatherReport>(result);
        }


    }
}

