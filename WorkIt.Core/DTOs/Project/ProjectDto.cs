using System;
using System.Collections.Generic;
using System.Text;

namespace WorkIt.Core.DTOs.Project
{
    public class ProjectDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public bool IsUserMember { get; set; }
        public bool IsOpenToJoin { get; set; }
    }
}
