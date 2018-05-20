using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WorkIt.Core.Constants;

namespace WorkIt.Web.Api.Utils
{
    public static class CrudStatusMapper
    {
        public static int MapCrudStatusToStatusCode(CrudStatus status)
        {
            switch (status)
            {
                case CrudStatus.BadRequest:
                    return StatusCodes.Status400BadRequest;
                case CrudStatus.Error:
                    return StatusCodes.Status500InternalServerError;
                case CrudStatus.Ok:
                    return StatusCodes.Status200OK;
                case CrudStatus.Created:
                    return StatusCodes.Status201Created;
                case CrudStatus.Deleted:
                    return StatusCodes.Status202Accepted;
                case CrudStatus.Updated:
                    return StatusCodes.Status202Accepted;
                case CrudStatus.Unauthorized:
                    return StatusCodes.Status401Unauthorized;
                default:
                    return StatusCodes.Status200OK;
            }
        }
    }
}