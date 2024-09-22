using System;
namespace WeatherApplication.Core.Results.Abstract
{
    public interface IDataResult<T>:IResult
    {
        T Data { get; }
    }
}

