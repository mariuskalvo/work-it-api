using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using WorkIt.Core.Entities;

namespace Core.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public IEnumerable<ApplicationUserOwnedProjects> OwnedProjects { get; set; }
        public IEnumerable<ApplicationUserProjectMembers> MemberProjects { get; set; }
    }
}
