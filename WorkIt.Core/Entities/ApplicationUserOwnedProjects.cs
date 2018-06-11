using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WorkIt.Core.Entities
{
    public class ApplicationUserOwnedProjects
    {
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public long ProjectId { get; set; }
        public Project Project { get; set; }
    }
}
