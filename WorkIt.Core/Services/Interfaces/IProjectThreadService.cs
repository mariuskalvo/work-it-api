using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Core.DTOs;

namespace Core.Services.Interfaces
{
    public interface IProjectThreadService
    {
        ProjectThreadDto Create(CreateProjectThreadDto groupThread, string creatorUserId);
        IEnumerable<ProjectThreadDto> GetLatestByProjectId(int limit, long threadId);
        IEnumerable<ProjectThreadDto> GetPagedByGroupId(long groupId, int page, int pageSize = 10);
    }
}
