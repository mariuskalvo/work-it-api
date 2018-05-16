using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Core.DTOs;
using WorkIt.Core.DTOs;

namespace Core.Services.Interfaces
{
    public interface IProjectThreadService
    {
        Task<CrudServiceResponse<ProjectThreadDto>> Create(CreateProjectThreadDto groupThread, string creatorUserId);
        Task<CrudServiceResponse<IEnumerable<ProjectThreadDto>>> GetPagedByProjectId(long threadId, int page, int pageSize);
    }
}
