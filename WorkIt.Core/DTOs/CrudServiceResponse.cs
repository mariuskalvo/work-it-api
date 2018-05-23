using System;
using System.Collections.Generic;
using System.Text;
using WorkIt.Core.Constants;

namespace WorkIt.Core.DTOs
{
    public class ServiceResponse
    {
        public ServiceStatus Status { get; set; }
        public string ErrorMessage { get; set; }
        public Exception Exception { get; set; }

        public ServiceResponse(ServiceStatus status, string errorMessage = null)
        {
            Status = status;
            ErrorMessage = errorMessage;
        }

        public ServiceResponse SetException(Exception exception)
        {
            Exception = exception;
            return this;
        }
    }

    public class ServiceResponse<T> : ServiceResponse where T : class
    {
        public T Data { get; set; }

        public ServiceResponse(ServiceStatus status, string errorMessage = null) : base(status, errorMessage)
        {
            Status = status;
            ErrorMessage = errorMessage;
        }

        public ServiceResponse<T> SetData(T data)
        {
            Data = data;
            return this;
        }

        public new ServiceResponse<T> SetException(Exception exception)
        {
            Exception = exception;
            return this;
        }
    }
}
