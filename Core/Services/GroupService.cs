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
using Microsoft.EntityFrameworkCore;
using Persistence.Database;

namespace Core.Services
{
    public class GroupService : IGroupService
    {
        private readonly IGroupRepository groupRepository;
        private readonly IMapper mapper;
        private readonly AppDbContext context;

        public GroupService(AppDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public GroupDto Create(CreateGroupDto createGroupDto)
        {
            var validator = new CreateGroupDtoValidator();
            var validationResults = validator.Validate(createGroupDto);

            if (!validationResults.IsValid)
                throw ExceptionFactory.CreateFromValidationResults(validationResults);

            var entityToAdd = mapper.Map<Group>(createGroupDto);
            entityToAdd.CreatedAt = DateTime.Now;

            context.Groups.Add(entityToAdd);
            context.SaveChanges();

            return mapper.Map<GroupDto>(entityToAdd); ;

        }

        public IEnumerable<GroupDto> Get(int limit)
        {
            var groups = context.Groups.Take(limit).ToList();
            return mapper.Map<IEnumerable<GroupDto>>(groups);
        }
    }
}
