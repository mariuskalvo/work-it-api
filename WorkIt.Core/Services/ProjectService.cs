using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Core.DTOs;
using Core.DTOS;
using Core.Entities;
using Core.Exceptions;
using Core.Validators;
using Microsoft.EntityFrameworkCore;
using WorkIt.Core.Entities;
using WorkIt.Core.Interfaces.Repositories;

namespace Core.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IMapper _mapper;
        private readonly IProjectRepository _projectRepository;

        public ProjectService(IProjectRepository projectRepository, IMapper mapper)
        {
            _mapper = mapper;
            _projectRepository = projectRepository;
        }

        public async Task<ProjectDto> Create(CreateProjectDto createGroupDto, string applicationUserId)
        {
            var validator = new CreateProjectDtoValidator();
            var validationResults = validator.Validate(createGroupDto);

            if (!validationResults.IsValid)
                return null;

            var entityToAdd = _mapper.Map<Project>(createGroupDto);

            entityToAdd.CreatedAt = DateTime.Now;
            entityToAdd.CreatedById = applicationUserId;

            var addedEntity = await _projectRepository.Create(entityToAdd);
            return _mapper.Map<ProjectDto>(addedEntity);
        }

        public async Task AddMemberToProject(long projectId, string userId)
        {
            await _projectRepository.AddMemberToProject(userId, projectId);
        }

        public async Task RemoveMemberFromProject(long projectId, string userId)
        {
            await _projectRepository.RemoveMemberFromProject(userId, projectId);
        }

        public async Task<IEnumerable<ProjectDto>> Get(int page, int pageSize)
        {
            int actualPage = Math.Max(page - 1, 0);
            int skip = actualPage * pageSize;

            var projects = await _projectRepository.GetProjects(pageSize, skip);
            return _mapper.Map<IEnumerable<ProjectDto>>(projects);
        }

    }
}
