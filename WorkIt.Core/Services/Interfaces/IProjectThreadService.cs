using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Core.DTOs;

namespace Core.Services.Interfaces
{
    public interface IProjectThreadService
    {
        Task<ProjectThreadDto> Create(CreateProjectThreadDto groupThread, string creatorUserId);
        Task<IEnumerable<ProjectThreadDto>> GetPagedByProjectId(long threadId, int page, int pageSize);
    }
}
