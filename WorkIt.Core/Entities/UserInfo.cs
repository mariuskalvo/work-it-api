using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using WorkIt.Core.Entities;

namespace WorkIt.Core.Entities
{
    public class UserInfo 
    {
        public long Id { get; set; }
        public string OpenIdSub { get; set; }

        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }

        public IEnumerable<ApplicationUserProjectMember> MemberProjects { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
