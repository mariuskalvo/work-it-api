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
using WorkIt.Core.Constants;
using WorkIt.Core.DTOs;
using WorkIt.Core.Entities;
using WorkIt.Core.Interfaces.Repositories;
using WorkIt.Core.Services.Interfaces;

namespace Core.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IMapper _mapper;
        private readonly IProjectRepository _projectRepository;
        private readonly IProjectMembershipRepository _projectMembershipRepository;

        public ProjectService(IProjectRepository projectRepository, 
                              IProjectMembershipRepository projectMembershipRepository,
                              IMapper mapper)
        {
            _mapper = mapper;
            _projectRepository = projectRepository;
            _projectMembershipRepository = projectMembershipRepository;
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

        public async Task<CrudServiceResponse> AddMemberToProject(long projectId, string userId)
        {
            try
            {
                var existingMembership = await _projectMembershipRepository.GetProjectMembership(projectId, userId);

                if (existingMembership != null)
                    return ServiceResponseStates.ErrorAttemptingAddingDuplicate();

                await _projectMembershipRepository.AddMemberToProject(userId, projectId);
                return ServiceResponseStates.OkResponse();

            } catch (Exception ex)
            {
                return ServiceResponseStates.ErrorResponse().SetException(ex);
            }
        }

        public async Task<CrudServiceResponse> RemoveMemberFromProject(long projectId, string userId)
        {
            try
            {
                var existingMembership = await _projectMembershipRepository.GetProjectMembership(projectId, userId);
                if (existingMembership == null)
                    return ServiceResponseStates.ErrorAttemptingRemoveNonExistingEntry();

                await _projectMembershipRepository.RemoveMembership(existingMembership);
                return ServiceResponseStates.OkResponse();

            } catch (Exception ex)
            {
                return ServiceResponseStates.ErrorResponse().SetException(ex);
            }
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
