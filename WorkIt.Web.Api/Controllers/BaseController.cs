﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using WorkIt.Core.Constants;
using WorkIt.Core.DTOs;
using WorkIt.Web.Api.Utils;

namespace WorkIt.Web.Api.Controllers
{
    public class BaseController : Controller
    {

        protected string GetOpenIdSub()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }

        protected IActionResult MapActionResultWithData<T>(ServiceResponse<T> response) where T : class
        {
            if (response.Status != ServiceStatus.Ok)
                return StatusCode(ServiceStatusMapper.MapToHttpStatusCode(response.Status));

            return new OkObjectResult(response.Data);
        }
    }
}