using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities;

namespace WorkIt.Core.Entities
{
    public class ApplicationUserProjectMembers
    {
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public long ProjectId { get; set; }
        public Project Project { get; set; }
    }
}
