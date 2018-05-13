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
using Core.DataAccess;
using WorkIt.Core.Entities;

namespace Core.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IMapper mapper;
        private readonly AppDbContext context;

        public ProjectService(AppDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public ProjectDto Create(CreateProjectDto createGroupDto, string applicationUserId)
        {
            var validator = new CreateProjectDtoValidator();
            var validationResults = validator.Validate(createGroupDto);

            if (!validationResults.IsValid)
                throw ExceptionFactory.CreateFromValidationResults(validationResults);

            var entityToAdd = mapper.Map<Project>(createGroupDto);

            entityToAdd.CreatedAt = DateTime.Now;
            entityToAdd.CreatedById = applicationUserId;

            context.Projects.Add(entityToAdd);
            context.SaveChanges();

            return mapper.Map<ProjectDto>(entityToAdd); ;

        }

        public void AddMemberToProject(long projectId, string userId)
        {
            context.ProjectMembers.Add(new ApplicationUserProjectMember() {
                ApplicationUserId = userId,
                ProjectId = projectId
            });

            context.SaveChanges();
        }

        public IEnumerable<ProjectDto> Get(int limit)
        {
            var groups = context.Projects.Take(limit).ToList();
            return mapper.Map<IEnumerable<ProjectDto>>(groups);
        }
    }
}
