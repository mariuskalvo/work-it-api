using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WorkIt.Core.Entities;
using WorkIt.Core.Interfaces.Repositories;
using WorkIt.Infrastructure.DataAccess;

namespace WorkIt.Infrastructure.Repositories
{
    public class UserInfoRepository : IUserInfoRepository
    {
        private readonly AppDbContext _dbContext;

        public UserInfoRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<UserInfo> GetUserInfoByOpenIdSub(string openIdSub)
        {
            return await _dbContext.UserInfos.Where(u => u.OpenIdSub == openIdSub).FirstOrDefaultAsync();
        }
    }
}
