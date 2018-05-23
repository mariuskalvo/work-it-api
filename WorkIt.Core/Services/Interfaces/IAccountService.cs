using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Core.DTOs;
using Core.Entities;
using WorkIt.Core.DTOs;

namespace Core.Services.Interfaces
{
    public interface IAccountService
    {
        Task<ServiceResponse> CreateAccount(CreateAccountDto createAccount);
        Task<ServiceResponse<string>> IssueToken(LoginDto loginDto);
    }
}
