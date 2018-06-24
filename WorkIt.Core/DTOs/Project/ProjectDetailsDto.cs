using System;
using System.Collections.Generic;
using System.Text;
using WorkIt.Core.DTOs.UserInfo;

namespace WorkIt.Core.DTOs.Project
{
    public class ProjectDetailsDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public bool IsUserMember { get; set; }
        public bool IsOpenToJoin { get; set; }

        public IEnumerable<SimpleUserInfoDto> Owners { get; set; }
        public IEnumerable<SimpleUserInfoDto> Members { get; set; }
        public SimpleUserInfoDto CreatedBy { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
