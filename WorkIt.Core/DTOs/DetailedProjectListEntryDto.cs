using System;
using System.Collections.Generic;
using System.Text;

namespace WorkIt.Core.DTOs
{
    public class DetailedProjectListEntryDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public bool IsUserMember { get; set; }
        public bool IsOpenToJoin { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedAt { get; set; }
    }
}
