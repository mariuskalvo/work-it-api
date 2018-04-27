using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Core.DTOs;
using Core.Entities;
using Core.RepositoryInterfaces;
using Core.Services.Interfaces;

namespace Core.Services
{
    public class ThreadEntryService : IThreadEntryService
    {
        private readonly IThreadEntryRepository threadEntryRepository;
        private readonly IMapper mapper;

        public ThreadEntryService(IThreadEntryRepository threadEntryRepository, IMapper mapper)
        {
            this.threadEntryRepository = threadEntryRepository;
            this.mapper = mapper;
        }

        public async Task<ThreadEntryDto> Create(CreateThreadEntryDto createDto)
        {
            var mappedEntity = mapper.Map<ThreadEntry>(createDto);

            mappedEntity.CreatedAt = DateTime.Now;
            var trackedEntity = await threadEntryRepository.Create(mappedEntity);

            return mapper.Map<ThreadEntryDto>(trackedEntity);
        }

        public async Task<IEnumerable<ThreadEntryDto>> GetByThreadId(long threadId)
        {
            var entities = await threadEntryRepository.GetByThreadId(threadId);
            return mapper.Map<IEnumerable<ThreadEntryDto>>(entities);
        }
    }
}
