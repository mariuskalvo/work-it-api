using System;
using System.Collections.Generic;
using System.Text;

namespace WorkIt.Core.DTOs.Project
{
    public class CreateProjectDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public bool PubliclyVisible { get; set; }
        public bool RequiresInvitation { get; set; }
    }
}
