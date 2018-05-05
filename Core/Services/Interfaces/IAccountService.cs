﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Core.DTOs;

namespace Core.Services.Interfaces
{
    public interface IAccountService
    {
        Task<ValidationResponse> CreateAccount(CreateAccountDto createAccount);
        Task<string> IssueToken(LoginDto loginDto);
    }
}
