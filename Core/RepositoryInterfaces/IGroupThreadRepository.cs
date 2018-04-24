using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Core.DTOs;
using Core.Entities;

namespace Core.RepositoryInterfaces
{
    public interface IGroupThreadRepository
    {
        Task<bool> Add(GroupThread groupThread);
        Task<IEnumerable<GroupThread>> GetLatest(int limit);
        Task<IEnumerable<GroupThread>> GetByGroupId(long groupId);
        Task<IEnumerable<GroupThread>> GetByGroupIdWithSkipAndLimit(long groupId, int take, int skip);
    }
}
