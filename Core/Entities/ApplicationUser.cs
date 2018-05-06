using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace Core.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public IEnumerable<ApplicationUserOwnedGroups> OwnedGroups { get; set; }
    }
}
