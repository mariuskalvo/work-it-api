using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities
{
    public class ApplicationUserOwnedGroups
    {
        public ApplicationUser ApplicationUser { get; set; }
        public long ApplicationUserId { get; set; }

        public Group Group { get; set; }
        public long GroupId { get; set; }
    }
}
