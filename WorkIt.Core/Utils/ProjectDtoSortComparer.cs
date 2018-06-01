using System;
using System.Collections.Generic;
using System.Text;
using Core.DTOS;

namespace WorkIt.Core.Utils
{
    public class ProjectDtoSortComparer : IComparer<ProjectDto>
    {
        public int Compare(ProjectDto p1, ProjectDto p2)
        {
            // Try to compare on user membership

            if (p2.IsUserMember && !p1.IsUserMember)
                return 1;

            if (!p2.IsUserMember && p1.IsUserMember)
                return -1;

            // Try to compare on project openness

            if (p2.IsOpenToJoin && !p1.IsOpenToJoin)
                return 1;

            if (!p2.IsOpenToJoin && p1.IsOpenToJoin)
                return -1;

            // Could not compare on user membership or 
            // project openness; Projects are equal

            return 0;
        }       
    }
}
