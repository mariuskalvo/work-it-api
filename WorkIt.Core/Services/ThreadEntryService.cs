using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Core.DTOs;
using Core.Entities;
using Core.Services.Interfaces;
using WorkIt.Core.Interfaces.Repositories;

namespace Core.Services
{
    public class ThreadEntryService : IThreadEntryService
    {
        private readonly IThreadEntryRepository _threadEntryRepository;
        private readonly IMapper _mapper;

        public ThreadEntryService(IThreadEntryRepository threadEntryRepository, IMapper mapper)
        {
            _threadEntryRepository = threadEntryRepository;
            _mapper = mapper;
        }

        public async Task<ThreadEntryDto> Create(CreateThreadEntryDto createDto, string currentUserId)
        {
            var entityToAdd = _mapper.Map<ThreadEntry>(createDto);

            entityToAdd.CreatedAt = DateTime.Now;
            entityToAdd.CreatedById = currentUserId;

            var created = await _threadEntryRepository.Create(entityToAdd);
            return _mapper.Map<ThreadEntryDto>(created);
        }

        public async Task<IEnumerable<ThreadEntryDto>> GetPagedByThreadId(long threadId, int page, int pageSize)
        {
            int actualPage = Math.Max(page - 1, 0);
            int skip = actualPage * pageSize;

            var entries = await _threadEntryRepository.GetByThreadId(threadId, pageSize, skip);
            return _mapper.Map<IEnumerable<ThreadEntryDto>>(entries);
        }

        public void AddReactionToThreadEntry(AddEntryReactionDto addReactionDto, string currentUserId)
        {

        }

    }
}
