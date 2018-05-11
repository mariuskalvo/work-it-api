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
using Core.DataAccess;

namespace Core.Services
{
    public class ProjectThreadService : IProjectThreadService
    {
        private readonly AppDbContext context;
        private readonly IMapper mapper;

        public ProjectThreadService(AppDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public ProjectThreadDto Create(CreateProjectThreadDto groupThread, string creatorUserId)
        {
            ProjectThreadValidator validator = new ProjectThreadValidator();
            var validationResults = validator.Validate(groupThread);

            if (!validationResults.IsValid)
                throw ExceptionFactory.CreateFromValidationResults(validationResults);

            var entityToAdd = mapper.Map<ProjectThread>(groupThread);

            entityToAdd.CreatedAt = DateTime.Now;
            entityToAdd.CreatedById = creatorUserId;

            context.Threads.Add(entityToAdd);
            context.SaveChanges();

            return mapper.Map<ProjectThreadDto>(entityToAdd);
        }

        public IEnumerable<ProjectThreadDto> GetLatestByProjectId(int limit, long projectId)
        {
            int maxToFetch = 10;
            int actualLimit = Math.Max(0, limit);
            actualLimit = Math.Min(maxToFetch, actualLimit);

            var entities = context.Threads.Where(t => t.ProjectId == projectId).OrderByDescending(grp => grp.CreatedAt).Take(actualLimit);
            return mapper.Map<IEnumerable<ProjectThreadDto>>(entities);
        }

        public IEnumerable<ProjectThreadDto> GetPagedByGroupId(long groupId, int page, int pageSize)
        {
            if (pageSize < 0)
                return new List<ProjectThreadDto>();

            int actualPage = Math.Max(page - 1, 0);
            int skip = actualPage * pageSize;

            var entities = context.Threads.Take(pageSize)
                                          .Skip(skip)
                                          .OrderByDescending(grp => grp.CreatedAt)
                                          .ToList();

            return mapper.Map<IEnumerable<ProjectThreadDto>>(entities);
        }
    }
}
