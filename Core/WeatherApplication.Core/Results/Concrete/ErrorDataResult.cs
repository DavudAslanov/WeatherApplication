using System;
namespace WeatherApplication.Core.Results.Concrete
{
    public class ErrorDataResult<T> : DataResult<T>
    {
        public ErrorDataResult(T data, bool issuccess) : base(data, issuccess)
        {
        }

        public ErrorDataResult(T data, string message, bool IsSuccess) : base(data, message, IsSuccess)
        {
        }
    }
}

