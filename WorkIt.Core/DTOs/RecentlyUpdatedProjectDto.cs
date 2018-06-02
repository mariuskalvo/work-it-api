using System;
using System.Collections.Generic;
using System.Text;

namespace WorkIt.Core.DTOs
{
    public class RecentlyUpdatedProjectDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
