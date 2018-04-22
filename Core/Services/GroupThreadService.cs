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

        public async Task<GroupThreadDto> Add(CreateGroupThreadDto groupThread)
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
    }
}
