using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WorkIt.Core.Entities;

namespace WorkIt.Core.Interfaces.Repositories
{
    public interface IUserInfoRepository
    {
        Task<UserInfo> GetUserInfoByOpenIdSub(string openIdSub);
        Task<UserInfo> CreateDefaultUserInfo(string openIdSub, string email);
        Task<IEnumerable<UserInfo>> GetProjectOwnersByProjectId(long projectId);
        Task<IEnumerable<UserInfo>> GetProjectMembersByProjectId(long projectId); 

    }
}
