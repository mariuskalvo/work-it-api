using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Core.DTOs;
using Core.DTOS;

namespace Core.Services
{
    public interface IProjectService
    {
        IEnumerable<ProjectDto> Get(int limit);
        ProjectDto Create(CreateProjectDto createGroupDto, string applicationUserId);
    }
}
