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

namespace Core.Services
{
    public class GroupThreadService : IGroupThreadService
    {
        private readonly IGroupThreadRepository groupThreadRepository;
        private readonly IMapper mapper;

        public GroupThreadService(IGroupThreadRepository groupThreadRepository, IMapper mapper)
        {
            this.groupThreadRepository = groupThreadRepository;
            this.mapper = mapper;
        }

        public async Task<GroupThreadDto> Create(CreateGroupThreadDto groupThread)
        {
            GroupThreadValidator validator = new GroupThreadValidator();
            var validationResults = validator.Validate(groupThread);

            if (!validationResults.IsValid)
                throw ExceptionFactory.CreateFromValidationResults(validationResults);

            var entityToAdd = mapper.Map<GroupThread>(groupThread);
            entityToAdd.CreatedAt = DateTime.Now;

            bool success = await groupThreadRepository.Add(entityToAdd);
            var addedDto = mapper.Map<GroupThreadDto>(entityToAdd);

            return addedDto;
        }

        public async Task<IEnumerable<GroupThreadDto>> GetLatest(int limit)
        {
            var entities = await groupThreadRepository.GetLatest(limit);
            return mapper.Map<IEnumerable<GroupThreadDto>>(entities);
        }

        public async Task<IEnumerable<GroupThreadDto>> GetPagedByGroupId(long groupId, int page, int pageSize = 10)
        {
            int actualPage = Math.Max(page - 1, 0);
            int skip = actualPage * pageSize;
            var entities = await groupThreadRepository.GetByGroupIdWithSkipAndLimit(groupId, pageSize, skip);
            return mapper.Map<IEnumerable<GroupThreadDto>>(entities);
        }
    }
}
