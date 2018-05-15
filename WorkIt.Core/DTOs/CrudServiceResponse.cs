using System;
using System.Collections.Generic;
using System.Text;
using WorkIt.Core.Constants;

namespace WorkIt.Core.DTOs
{
    public class CrudServiceResponse
    {
        public CrudStatus Status { get; set; }
        public string ErrorMessage { get; set; }
        public Exception Exception { get; set; }

        public CrudServiceResponse(CrudStatus status, string errorMessage = null)
        {
            Status = status;
            ErrorMessage = errorMessage;
        }

        public CrudServiceResponse SetException(Exception exception)
        {
            Exception = exception;
            return this;
        }
    }

    public class CrudServiceResponse<T> : CrudServiceResponse where T : class
    {
        public T Data { get; set; }

        public CrudServiceResponse(CrudStatus status, string errorMessage = null) : base(status, errorMessage)
        {
            Status = status;
            ErrorMessage = errorMessage;
        }

        public CrudServiceResponse<T> SetData(T data)
        {
            Data = data;
            return this;
        }
    }
}
