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
}
