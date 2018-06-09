using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DTOs
{
    public class CreateProjectDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public bool PubliclyVisible { get; set; }
        public bool RequiresInvitation { get; set; }
    }
}
