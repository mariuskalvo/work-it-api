using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WorkIt.Core.Entities
{
    public class ApplicationUserOwnedProjects
    {
        public long UserInfoId { get; set; }
        public UserInfo UserInfo { get; set; }

        public long ProjectId { get; set; }
        public Project Project { get; set; }
    }
}
