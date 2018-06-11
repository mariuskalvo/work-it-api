﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Core.DTOs;
using Core.Exceptions;
using Core.Services.Interfaces;
using Core.Validators;
using Microsoft.EntityFrameworkCore;
using WorkIt.Core.Constants;
using WorkIt.Core.DTOs;
using WorkIt.Core.DTOs.Project;
using WorkIt.Core.Entities;
using WorkIt.Core.Interfaces.Repositories;
using WorkIt.Core.Services.Interfaces;
using WorkIt.Core.Utils;

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
            entityToAdd.ModifiedAt = DateTime.Now;

            entityToAdd.CreatedById = applicationUserId;

            var addedEntity = await _projectRepository.Create(entityToAdd);
            var projectDto = _mapper.Map<ProjectDto>(addedEntity);

            return new ServiceResponse<ProjectDto>(ServiceStatus.Ok).SetData(projectDto);
        }

        public async Task<ServiceResponse> AddMemberToProject(string currentUserId, long projectId, string userIdToBeAdded)
        {
            try
            {
                var userProjectOwnership = await _projectRepository.GetProjectsOwnership(currentUserId, projectId);
                if (userProjectOwnership == null)
                    return new ServiceResponse(ServiceStatus.Unauthorized);

                var user = await _userService.GetUserById(userIdToBeAdded);
                if (user == null)
                    return new ServiceResponse(ServiceStatus.BadRequest);

                var project = await _projectRepository.GetById(projectId);
                if (project == null)
                    return new ServiceResponse(ServiceStatus.BadRequest);

                var existingMembership = await _projectMembershipRepository.GetProjectMembership(projectId, userIdToBeAdded);
                if (existingMembership != null)
                    return new ServiceResponse(ServiceStatus.BadRequest);

                await _projectMembershipRepository.AddMemberToProject(userIdToBeAdded, projectId);
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
                projectDtos.Sort(new ProjectDtoSortComparer());

                return new ServiceResponse<IEnumerable<ProjectDto>>(ServiceStatus.Ok).SetData(projectDtos);
            } catch (Exception ex)
            {
                return new ServiceResponse<IEnumerable<ProjectDto>>(ServiceStatus.Error);
            }
        }

        public async Task<ServiceResponse<IEnumerable<RecentlyUpdatedProjectDto>>> GetLastUpdatedProjects(string currentUserId, int limit)
        {
            try
            {
                var entities = await _projectRepository.GetLastUpdatedUserProjects(currentUserId, limit);
                var dtos = _mapper.Map<IEnumerable<RecentlyUpdatedProjectDto>>(entities);
                return new ServiceResponse<IEnumerable<RecentlyUpdatedProjectDto>>(ServiceStatus.Ok).SetData(dtos);

            } catch (Exception)
            {
                return new ServiceResponse<IEnumerable<RecentlyUpdatedProjectDto>>(ServiceStatus.Error);
            }
        }

        public async Task<ServiceResponse<IEnumerable<DetailedProjectListEntryDto>>> GetDetailedProjects(string currentUserId)
        {
            try
            {
                var entities = await _projectRepository.GetProjects();
                var projectsWithMembership = await _projectRepository.GetMemberProjectsForUser(currentUserId);
                var projectsWithMembershipIds = projectsWithMembership.Select(p => p.Id);

                var dtos = _mapper.Map<IEnumerable<DetailedProjectListEntryDto>>(entities).ToList();
                dtos.ForEach(p => p.IsUserMember = projectsWithMembershipIds.Contains(p.Id));

                return new ServiceResponse<IEnumerable<DetailedProjectListEntryDto>>(ServiceStatus.Ok).SetData(dtos);
            }
            catch (Exception)
            {
                return new ServiceResponse<IEnumerable<DetailedProjectListEntryDto>>(ServiceStatus.Error);
            }
        }

        public async Task<ServiceResponse<ProjectDetailsDto>> GetProjectDetailsByProjectId(long projectId, string userId)
        {
            var projectMembership = await _projectMembershipRepository.GetProjectMembership(projectId, userId);
            var project = await _projectRepository.GetById(projectId);

            if (projectMembership == null && !project.IsPubliclyVisible)
            {
                return new ServiceResponse<ProjectDetailsDto>(ServiceStatus.Unauthorized);
            }

            var projectOwnerships = await _projectMembershipRepository.GetProjectOwnersByProjectId(projectId);
            var projectMemberships = await _projectMembershipRepository.GetProjectMembershipsByProjectId(projectId);

            var projectOwners = projectOwnerships.Select(po => po.ApplicationUser);
            var projectMembers = projectMemberships.Select(pm => pm.ApplicationUser);

            var projectDetailsDto = _mapper.Map<ProjectDetailsDto>(project);

            return new ServiceResponse<ProjectDetailsDto>(ServiceStatus.Ok).SetData(projectDetailsDto);
        }
    }
}
