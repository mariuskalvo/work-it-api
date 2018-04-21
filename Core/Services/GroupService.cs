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

namespace Core.Services
{
    public class GroupService : IGroupService
    {
        private readonly IMapper mapper;

        public GroupService(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public static List<Group> groups = new List<Group>()
        {
            new Group() { Id = 0, Title = "First group"},
            new Group() { Id = 1, Title = "Second group"}
        };

        public Task<GroupDto> Create(CreateGroupDto createGroupDto)
        {
            var validator = new CreateGroupDtoValidator();
            var validationResults = validator.Validate(createGroupDto);

            if (!validationResults.IsValid)
            {
                var errors = validationResults.Errors.Select(error => error.PropertyName);
                var errorListString = string.Join(",", errors);
                throw new InvalidModelStateException($"Could not create new category: The following properties are invalid: {errorListString}");
            }

            return Task.Run(() =>
            {
                var groupToAdd = mapper.Map<Group>(createGroupDto);
                groupToAdd.Id = groups.Count;

                groups.Add(groupToAdd);

                var groupToReturn = mapper.Map<GroupDto>(groupToAdd);

                return groupToReturn;
            });
        }

        public async Task<IEnumerable<GroupDto>> Get(int limit)
        {
            return mapper.Map<IEnumerable<GroupDto>>(groups);
        }
    }
}
