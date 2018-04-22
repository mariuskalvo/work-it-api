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
using Core.RepositoryInterfaces;
using Core.Validators;

namespace Core.Services
{
    public class GroupService : IGroupService
    {
        private readonly IGroupRepository groupRepository;
        private readonly IMapper mapper;

        public GroupService(IGroupRepository groupRepository, IMapper mapper)
        {
            this.groupRepository = groupRepository;
            this.mapper = mapper;
        }

        public async Task<GroupDto> Create(CreateGroupDto createGroupDto)
        {
            var validator = new CreateGroupDtoValidator();
            var validationResults = validator.Validate(createGroupDto);

            if (!validationResults.IsValid)
                throw ExceptionFactory.CreateFromValidationResults(validationResults);

            var entityToAdd = mapper.Map<Group>(createGroupDto);
            entityToAdd.CreatedAt = DateTime.Now;

            var trackedEntity = await groupRepository.Add(entityToAdd);
            var createdGroupDto = mapper.Map<GroupDto>(trackedEntity);

            return createdGroupDto;
        }

        public async Task<IEnumerable<GroupDto>> Get(int limit)
        {
            var groups = await groupRepository.GetAll();
            return mapper.Map<IEnumerable<GroupDto>>(groups);
        }
    }
}
