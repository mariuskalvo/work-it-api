using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Core.DTOs;

namespace Core.Services.Interfaces
{
    public interface IGroupThreadService
    {
        GroupThreadDto Create(CreateGroupThreadDto groupThread);
        IEnumerable<GroupThreadDto> GetLatest(int limit);
        IEnumerable<GroupThreadDto> GetPagedByGroupId(long groupId, int page, int pageSize = 10);
    }
}
