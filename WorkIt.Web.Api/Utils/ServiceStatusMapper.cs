using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using WorkIt.Core.Constants;

namespace WorkIt.Web.Api.Utils
{
    public static class ServiceStatusMapper
    {
        public static int MapToHttpStatusCode(ServiceStatus status)
        {
            switch (status)
            {
                case ServiceStatus.BadRequest:
                    return StatusCodes.Status400BadRequest;
                case ServiceStatus.Error:
                    return StatusCodes.Status500InternalServerError;
                case ServiceStatus.Ok:
                    return StatusCodes.Status200OK;
                case ServiceStatus.Created:
                    return StatusCodes.Status201Created;
                case ServiceStatus.Deleted:
                    return StatusCodes.Status202Accepted;
                case ServiceStatus.Updated:
                    return StatusCodes.Status202Accepted;
                case ServiceStatus.Unauthorized:
                    return StatusCodes.Status401Unauthorized;
                default:
                    return StatusCodes.Status200OK;
            }
        }
    }
}