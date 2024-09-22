using System;
using WeatherApplication.Core.Results.Abstract;

namespace WeatherApplication.Core.Results.Concrete
{
    public class DataResult<T>:Result,IDataResult<T>
    {
        public DataResult(T data,bool issuccess):base(issuccess)
        {
            Data = data;
        }
        public DataResult(T data,string message, bool IsSuccess) : base(message,IsSuccess)
        {
            Data = data;
        }
        
        public T Data { get; }
    }
}