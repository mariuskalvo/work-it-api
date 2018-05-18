using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Core.DTOs;
using Core.Entities;
using Core.Exceptions;
using Core.Services.Interfaces;
using Core.Validators;
using WorkIt.Core.Constants;
using WorkIt.Core.DTOs;
using WorkIt.Core.Interfaces.Repositories;

namespace Core.Services
{
    public class ProjectThreadService : IProjectThreadService
    {
        private readonly IProjectThreadRepository _projectThreadRepository;
        private readonly IMapper _mapper;

        public ProjectThreadService(IProjectThreadRepository projectThreadRepository, IMapper mapper)
        {
            _projectThreadRepository = projectThreadRepository;
            _mapper = mapper;
        }

        public async Task<CrudServiceResponse<ProjectThreadDto>> Create(CreateProjectThreadDto groupThread, string creatorUserId)
        {
            ProjectThreadValidator validator = new ProjectThreadValidator();
            var validationResults = validator.Validate(groupThread);

            if (!validationResults.IsValid)
                return null;

            var entityToAdd = _mapper.Map<ProjectThread>(groupThread);
            entityToAdd.CreatedAt = DateTime.Now;
            entityToAdd.CreatedById = creatorUserId;

            try
            {
                var addedEntity = await _projectThreadRepository.Create(entityToAdd);
                var returningDto = _mapper.Map<ProjectThreadDto>(entityToAdd);
                return new CrudServiceResponse<ProjectThreadDto>(CrudStatus.Ok).SetData(returningDto);
            } catch (Exception ex)
            {
                return new CrudServiceResponse<ProjectThreadDto>(CrudStatus.Error).SetException(ex);
            }
        }

        public async Task<CrudServiceResponse<IEnumerable<ProjectThreadDto>>> GetPagedByProjectId(long projectId, int page, int pageSize)
        {
            int actualPage = Math.Max(page - 1, 0);
            int skip = actualPage * pageSize;

            try
            {
                var entities = await _projectThreadRepository.GetProjectThreads(projectId, pageSize, skip);
                var returningDtos = _mapper.Map<IEnumerable<ProjectThreadDto>>(entities);
                return new CrudServiceResponse<IEnumerable<ProjectThreadDto>>(CrudStatus.Ok).SetData(returningDtos);
            }
            catch (Exception ex)
            {
                return new CrudServiceResponse<IEnumerable<ProjectThreadDto>>(CrudStatus.Error).SetException(ex);
            }
        }
    }
}
