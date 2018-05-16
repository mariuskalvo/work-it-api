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

        public async Task<CrudServiceResponse<ProjectDto>> Create(CreateProjectDto createGroupDto, string applicationUserId)
        {
            var validator = new CreateProjectDtoValidator();
            var validationResults = validator.Validate(createGroupDto);

            if (!validationResults.IsValid)
                return new CrudServiceResponse<ProjectDto>(CrudStatus.BadRequest);

            var entityToAdd = _mapper.Map<Project>(createGroupDto);

            entityToAdd.CreatedAt = DateTime.Now;
            entityToAdd.CreatedById = applicationUserId;

            var addedEntity = await _projectRepository.Create(entityToAdd);
            var projectDto = _mapper.Map<ProjectDto>(addedEntity);

            return new CrudServiceResponse<ProjectDto>(CrudStatus.Ok).SetData(projectDto);
        }

        public async Task<CrudServiceResponse> AddMemberToProject(long projectId, string userId)
        {
            try
            {
                var user = await _userService.GetUserById(userId);
                if (user == null)
                    return new CrudServiceResponse(CrudStatus.BadRequest);

                var project = await _projectRepository.GetById(projectId);
                if (project == null)
                    return new CrudServiceResponse(CrudStatus.BadRequest);

                var existingMembership = await _projectMembershipRepository.GetProjectMembership(projectId, userId);
                if (existingMembership != null)
                    return new CrudServiceResponse(CrudStatus.BadRequest);

                await _projectMembershipRepository.AddMemberToProject(userId, projectId);
                return new CrudServiceResponse(CrudStatus.Ok);

            } catch (Exception ex)
            {
                return new CrudServiceResponse(CrudStatus.Error).SetException(ex);
            }
        }

        public async Task<CrudServiceResponse> RemoveMemberFromProject(long projectId, string userId)
        {
            try
            {
                var existingMembership = await _projectMembershipRepository.GetProjectMembership(projectId, userId);
                if (existingMembership == null)
                    return new CrudServiceResponse(CrudStatus.BadRequest, "Attempting to remove non-existing project membership");

                await _projectMembershipRepository.RemoveMembership(existingMembership);
                return new CrudServiceResponse(CrudStatus.Ok);

            } catch (Exception ex)
            {
                return new CrudServiceResponse(CrudStatus.Error).SetException(ex);
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
