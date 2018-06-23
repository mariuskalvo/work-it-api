using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WorkIt.Core.DTOs;
using WorkIt.Core.DTOs.ProjectThread;

namespace Core.Services.Interfaces
{
    public interface IProjectThreadService
    {
        Task<ServiceResponse<ProjectThreadOverviewDto>> Create(CreateProjectThreadDto groupThread, string creatorOpenIdSubject);
        Task<ServiceResponse<IEnumerable<ProjectThreadOverviewDto>>> GetPagedByProjectId(long threadId, int page, string currentUserOpenIdSub);
        Task<ServiceResponse<ProjectThreadDto>> GetByThreadId(long threadId, string currentUserOpenIdSub);
    }
}
