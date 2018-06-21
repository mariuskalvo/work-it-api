using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Core.DTOs;
using Core.Exceptions;
using Core.Services.Interfaces;
using Core.Validators;
using WorkIt.Core.Constants;
using WorkIt.Core.DTOs;
using WorkIt.Core.Entities;
using WorkIt.Core.Interfaces.Repositories;
using WorkIt.Core.Services.Interfaces;

namespace Core.Services
{
    public class ProjectThreadService : IProjectThreadService
    {
        private readonly IProjectThreadRepository _projectThreadRepository;
        private readonly IProjectMembershipRepository _projectMembershipRepository;
        private readonly IUserInfoRepository _userInfoRepository;
        private readonly IMapper _mapper;

        public ProjectThreadService(
            IProjectThreadRepository projectThreadRepository,
            IProjectMembershipRepository projectMembershipRepository,
            IUserInfoRepository userInfoRepository,
            IMapper mapper)
        {
            _projectThreadRepository = projectThreadRepository;
            _projectMembershipRepository = projectMembershipRepository;
            _userInfoRepository = userInfoRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<ProjectThreadDto>> Create(CreateProjectThreadDto groupThread, string creatorOpenIdSubject)
        {
            ProjectThreadValidator validator = new ProjectThreadValidator();
            var validationResults = validator.Validate(groupThread);

            if (!validationResults.IsValid)
                return new ServiceResponse<ProjectThreadDto>(ServiceStatus.BadRequest);

            var userInfo = await _userInfoRepository.GetUserInfoByOpenIdSub(creatorOpenIdSubject);
            if (userInfo == null)
                return new ServiceResponse<ProjectThreadDto>(ServiceStatus.Error);

            var projectMembership = await _projectMembershipRepository.GetProjectMembership(groupThread.ProjectId, userInfo.Id);
            if (projectMembership == null)
                return new ServiceResponse<ProjectThreadDto>(ServiceStatus.Unauthorized);

            var entityToAdd = _mapper.Map<ProjectThread>(groupThread);
            entityToAdd.CreatedAt = DateTime.Now;
            entityToAdd.CreatedById = creatorOpenIdSubject;

            try
            {
                var addedEntity = await _projectThreadRepository.Create(entityToAdd);
                var returningDto = _mapper.Map<ProjectThreadDto>(entityToAdd);
                return new ServiceResponse<ProjectThreadDto>(ServiceStatus.Ok).SetData(returningDto);
            } catch (Exception ex)
            {
                return new ServiceResponse<ProjectThreadDto>(ServiceStatus.Error).SetException(ex);
            }
        }

        public async Task<ServiceResponse<IEnumerable<ProjectThreadDto>>> GetPagedByProjectId(long projectId, int page, int pageSize)
        {
            int actualPage = Math.Max(page - 1, 0);
            int skip = actualPage * pageSize;

            try
            {
                var entities = await _projectThreadRepository.GetProjectThreads(projectId, pageSize, skip);
                var returningDtos = _mapper.Map<IEnumerable<ProjectThreadDto>>(entities);
                return new ServiceResponse<IEnumerable<ProjectThreadDto>>(ServiceStatus.Ok).SetData(returningDtos);
            }
            catch (Exception ex)
            {
                return new ServiceResponse<IEnumerable<ProjectThreadDto>>(ServiceStatus.Error).SetException(ex);
            }
        }
    }
}
