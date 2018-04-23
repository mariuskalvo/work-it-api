using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Core.DTOs;

namespace Core.Services.Interfaces
{
    public interface IGroupThreadService
    {
        Task<GroupThreadDto> Create(CreateGroupThreadDto groupThread);
    }
}
