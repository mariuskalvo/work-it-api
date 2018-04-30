using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Core.DTOs;
using Core.Entities;
using Core.Exceptions;
using Core.RepositoryInterfaces;
using Core.Services.Interfaces;
using Core.Validators;
using Persistence.Database;

namespace Core.Services
{
    public class GroupThreadService : IGroupThreadService
    {
        private readonly AppDbContext context;
        private readonly IMapper mapper;

        public GroupThreadService(AppDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public GroupThreadDto Create(CreateGroupThreadDto groupThread)
        {
            GroupThreadValidator validator = new GroupThreadValidator();
            var validationResults = validator.Validate(groupThread);

            if (!validationResults.IsValid)
                throw ExceptionFactory.CreateFromValidationResults(validationResults);

            var entityToAdd = mapper.Map<GroupThread>(groupThread);
            entityToAdd.CreatedAt = DateTime.Now;

            context.Threads.Add(entityToAdd);
            context.SaveChanges();

            return mapper.Map<GroupThreadDto>(entityToAdd);
        }

        public IEnumerable<GroupThreadDto> GetLatest(int limit)
        {
            int maxToFetch = 10;
            int actualLimit = Math.Max(0, limit);
            actualLimit = Math.Min(maxToFetch, actualLimit);

            var entities = context.Threads.Take(limit).OrderByDescending(grp => grp.CreatedAt);
            return mapper.Map<IEnumerable<GroupThreadDto>>(entities);
        }

        public IEnumerable<GroupThreadDto> GetPagedByGroupId(long groupId, int page, int pageSize)
        {
            if (pageSize < 0)
                return new List<GroupThreadDto>();

            int actualPage = Math.Max(page - 1, 0);
            int skip = actualPage * pageSize;

            var entities = context.Threads.Take(pageSize)
                                          .Skip(skip)
                                          .OrderByDescending(grp => grp.CreatedAt)
                                          .ToList();

            return mapper.Map<IEnumerable<GroupThreadDto>>(entities);
        }
    }
}
