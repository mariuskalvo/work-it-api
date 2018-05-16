using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;
using Core.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Core.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> userManager;

        public UserService(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }
        public async Task<string> GetCurrentUserId(string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            return user?.Id;
        }

        public async Task<ApplicationUser> GetUserById(string userId)
        {
            return await userManager.FindByIdAsync(userId);
        }
    }
}
