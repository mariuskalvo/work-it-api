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
using Core.Services.Interfaces;
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
        private readonly IUserService _userService;

        public ProjectService(IProjectRepository projectRepository, 
                              IProjectMembershipRepository projectMembershipRepository,
                              IUserService userService,
                              IMapper mapper)
        {
            _mapper = mapper;
            _projectRepository = projectRepository;
            _projectMembershipRepository = projectMembershipRepository;
            _userService = userService;
        }

        public async Task<ServiceResponse<ProjectDto>> Create(CreateProjectDto createGroupDto, string applicationUserId)
        {
            var validator = new CreateProjectDtoValidator();
            var validationResults = validator.Validate(createGroupDto);

            if (!validationResults.IsValid)
                return new ServiceResponse<ProjectDto>(ServiceStatus.BadRequest);

            var entityToAdd = _mapper.Map<Project>(createGroupDto);

            entityToAdd.CreatedAt = DateTime.Now;
            entityToAdd.CreatedById = applicationUserId;

            var addedEntity = await _projectRepository.Create(entityToAdd);
            var projectDto = _mapper.Map<ProjectDto>(addedEntity);

            return new ServiceResponse<ProjectDto>(ServiceStatus.Ok).SetData(projectDto);
        }

        public async Task<ServiceResponse> AddMemberToProject(long projectId, string userId)
        {
            try
            {
                var user = await _userService.GetUserById(userId);
                if (user == null)
                    return new ServiceResponse(ServiceStatus.BadRequest);

                var project = await _projectRepository.GetById(projectId);
                if (project == null)
                    return new ServiceResponse(ServiceStatus.BadRequest);

                var existingMembership = await _projectMembershipRepository.GetProjectMembership(projectId, userId);
                if (existingMembership != null)
                    return new ServiceResponse(ServiceStatus.BadRequest);

                await _projectMembershipRepository.AddMemberToProject(userId, projectId);
                return new ServiceResponse(ServiceStatus.Ok);

            } catch (Exception ex)
            {
                return new ServiceResponse(ServiceStatus.Error).SetException(ex);
            }
        }

        public async Task<ServiceResponse> RemoveMemberFromProject(long projectId, string userId)
        {
            try
            {
                var existingMembership = await _projectMembershipRepository.GetProjectMembership(projectId, userId);
                if (existingMembership == null)
                    return new ServiceResponse(ServiceStatus.BadRequest, "Attempting to remove non-existing project membership");

                await _projectMembershipRepository.RemoveMembership(existingMembership);
                return new ServiceResponse(ServiceStatus.Ok);

            } catch (Exception ex)
            {
                return new ServiceResponse(ServiceStatus.Error).SetException(ex);
            }
        }

        public async Task<ServiceResponse<IEnumerable<ProjectDto>>> GetMemberProjectsForUser(string currentUserId)
        {
            try
            {
                var projects = await _projectRepository.GetMemberProjectsForUser(currentUserId);
                var projectDtos = _mapper.Map<IEnumerable<ProjectDto>>(projects);
                return new ServiceResponse<IEnumerable<ProjectDto>>(ServiceStatus.Ok).SetData(projectDtos);
            }
            catch (Exception ex)
            {
                return new ServiceResponse<IEnumerable<ProjectDto>>(ServiceStatus.Error);
            }
        }

        public async Task<ServiceResponse<IEnumerable<ProjectDto>>> GetProjects(string currentUserId)
        {
            try
            {
                var projects = await _projectRepository.GetProjects();
                var projectsWithMembership = await _projectRepository.GetMemberProjectsForUser(currentUserId);

                var projectsWithMembershipIds = projectsWithMembership.Select(p => p.Id);

                var projectDtos = _mapper.Map<IEnumerable<ProjectDto>>(projects).ToList();

                projectDtos.ForEach(p => p.IsUserMember = projectsWithMembershipIds.Contains(p.Id));

                return new ServiceResponse<IEnumerable<ProjectDto>>(ServiceStatus.Ok).SetData(projectDtos);
            } catch (Exception ex)
            {
                return new ServiceResponse<IEnumerable<ProjectDto>>(ServiceStatus.Error);
            }
        }
    }
}
