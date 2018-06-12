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
    }
}
