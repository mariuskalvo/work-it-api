using System;
using System.Collections.Generic;
using System.Text;
using WorkIt.Core.DTOs;

namespace WorkIt.Core.Constants
{
    public static class ServiceResponseStates
    {
        public static CrudServiceResponse ErrorAttemptingRemoveNonExistingEntry() 
            => new CrudServiceResponse(CrudStatus.BadRequest, "The resource you attempted to remove does not exist");

        public static CrudServiceResponse ErrorAttemptingAddingDuplicate()
            => new CrudServiceResponse(CrudStatus.BadRequest, "The resource you attempted to add already exists");

        public static CrudServiceResponse ErrorResponse(string error = null) 
            => new CrudServiceResponse(CrudStatus.Error, error);

        public static CrudServiceResponse OkResponse() => 
            new CrudServiceResponse(CrudStatus.Ok);
    }
}
