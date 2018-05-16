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
        Task<CrudServiceResponse> CreateAccount(CreateAccountDto createAccount);
        Task<CrudServiceResponse<string>> IssueToken(LoginDto loginDto);
    }
}
