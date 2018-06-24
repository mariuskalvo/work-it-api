﻿using System;
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

        public async Task<UserInfo> CreateDefaultUserInfo(string openIdSub)
        {
            var userInfo = new UserInfo
            {
                OpenIdSub = openIdSub,
                CreatedAt = DateTime.Now
            };
            _dbContext.UserInfos.Add(userInfo);
            await _dbContext.SaveChangesAsync();
            return userInfo;
        }

        public async Task<IEnumerable<UserInfo>> GetProjectMembersByProjectId(long projectId)
        {
            return await (from userInfo in _dbContext.UserInfos
                    join projectMember in _dbContext.ProjectMembers
                    on userInfo.Id equals projectMember.UserInfoId
                    where projectMember.ProjectId == projectId
                    select userInfo).ToListAsync();
        }

        public async Task<IEnumerable<UserInfo>> GetProjectOwnersByProjectId(long projectId)
        {
            return await (from userInfo in _dbContext.UserInfos
                         join projectOwner in _dbContext.ProjectOwners
                         on userInfo.Id equals projectOwner.UserInfoId
                         where projectOwner.ProjectId == projectId
                         select userInfo).ToListAsync();
        }

        public async Task<UserInfo> GetUserInfoByOpenIdSub(string openIdSub)
        {
            return await _dbContext
                .UserInfos
                .Where(u => u.OpenIdSub == openIdSub)
                .FirstOrDefaultAsync();
        }
    }
}
