using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Core.DTOs;
using WorkIt.Core.DTOs;
using WorkIt.Core.DTOs.Auth0;

namespace Core.Services.Interfaces
{
    public interface IAccountService
    {
        Task<ServiceResponse<string>> Login(LoginDto loginDto);
    }
}
