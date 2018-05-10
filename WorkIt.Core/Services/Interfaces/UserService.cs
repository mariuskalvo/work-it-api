using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Services.Interfaces
{
    public interface IUserService
    {
        Task<string> GetCurrentUserId(string email);
    }
}
