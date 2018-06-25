using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.Validators;
using Microsoft.EntityFrameworkCore;
using WorkIt.Core.Constants;
using WorkIt.Core.DTOs;
using WorkIt.Core.DTOs.Project;
using WorkIt.Core.DTOs.UserInfo;
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
        private readonly IUserInfoRepository _userInfoRepository;

        public ProjectService(IProjectRepository projectRepository, 
                              IProjectMembershipRepository projectMembershipRepository,
                              IUserInfoRepository userInfoRepository,
                              IMapper mapper)
        {
            _mapper = mapper;
            _projectRepository = projectRepository;
            _projectMembershipRepository = projectMembershipRepository;
            _userInfoRepository = userInfoRepository;
        }

        public async Task<ServiceResponse<ProjectDto>> Create(CreateProjectDto createProjectDto, string applicationUserId)
        {
            var validator = new CreateProjectDtoValidator();
            var validationResults = validator.Validate(createProjectDto);

            if (!validationResults.IsValid)
                return new ServiceResponse<ProjectDto>(ServiceStatus.BadRequest);

            var entityToAdd = _mapper.Map<Project>(createProjectDto);

            var userInfo = await _userInfoRepository.GetUserInfoByOpenIdSub(applicationUserId);
            if (userInfo == null)
                return new ServiceResponse<ProjectDto>(ServiceStatus.BadRequest);

            entityToAdd.CreatedById = userInfo.Id;
            entityToAdd.CreatedAt = DateTime.Now;
            entityToAdd.ModifiedAt = DateTime.Now;

            var addedEntity = await _projectRepository.Create(entityToAdd);
            await _projectMembershipRepository.AddMemberToProject(userInfo.Id, addedEntity.Id, RoleLevel.Owner);

            var projectDto = _mapper.Map<ProjectDto>(addedEntity);
            return new ServiceResponse<ProjectDto>(ServiceStatus.Ok).SetData(projectDto);
        }

        public async Task<ServiceResponse> AddMemberToProject(string currentUserId, long projectId, long userIdToBeAdded)
        {
            try
            {
                var userInfo = await _userInfoRepository.GetUserInfoByOpenIdSub(currentUserId);
                if (userInfo == null)
                    return new ServiceResponse(ServiceStatus.BadRequest);

                var userProjectOwnership = await _projectMembershipRepository.GetProjectMembership(projectId, userInfo.Id);
                if (userProjectOwnership == null || userProjectOwnership.RoleLevel == RoleLevel.Owner)
                    return new ServiceResponse(ServiceStatus.Unauthorized);

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

        public async Task<ServiceResponse> RemoveMemberFromProject(long projectId, long userId)
        {
            try
            {
                var existingMembership = await _projectMembershipRepository.GetProjectMembership(projectId, userId);
                if (existingMembership == null)
                    return new ServiceResponse(ServiceStatus.BadRequest);

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
            var userInfo = _userInfoRepository.GetUserInfoByOpenIdSub(userId);
            if (userInfo == null)
            {
                return new ServiceResponse<ProjectDetailsDto>(ServiceStatus.BadRequest);
            }

            var project = await _projectRepository.GetById(projectId);
            var projectMembership = await _projectMembershipRepository.GetProjectMembership(projectId, userInfo.Id);

            if (projectMembership == null && !project.IsPubliclyVisible)
            {
                return new ServiceResponse<ProjectDetailsDto>(ServiceStatus.Unauthorized);
            }

            var projectMembers = await _userInfoRepository.GetProjectMembersByProjectId(projectId);

            var memberDtos = _mapper.Map<IEnumerable<SimpleUserInfoDto>>(projectMembers);
            var projectDetailsDto = _mapper.Map<ProjectDetailsDto>(project);
            projectDetailsDto.Members = memberDtos;

            return new ServiceResponse<ProjectDetailsDto>(ServiceStatus.Ok).SetData(projectDetailsDto);
        }
    }
}
