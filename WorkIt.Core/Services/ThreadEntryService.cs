using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Core.DTOs;
using Core.Entities;
using Core.Services.Interfaces;
using WorkIt.Core.Constants;
using WorkIt.Core.DTOs;
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

        public async Task<ServiceResponse<ThreadEntryDto>> Create(CreateThreadEntryDto createDto, string currentUserId)
        {
            var entityToAdd = _mapper.Map<ThreadEntry>(createDto);
            entityToAdd.CreatedAt = DateTime.Now;
            entityToAdd.CreatedById = currentUserId;

            try
            {
                var created = await _threadEntryRepository.Create(entityToAdd);
                var returningDto = _mapper.Map<ThreadEntryDto>(created);
                return new ServiceResponse<ThreadEntryDto>(ServiceStatus.Ok).SetData(returningDto);
            } catch (Exception ex)
            {
                return new ServiceResponse<ThreadEntryDto>(ServiceStatus.Error).SetException(ex);
            }

        }

        public async Task<ServiceResponse<IEnumerable<ThreadEntryDto>>> GetPagedByThreadId(long threadId, int page, int pageSize)
        {
            int actualPage = Math.Max(page - 1, 0);
            int skip = actualPage * pageSize;

            try
            {
                var entries = await _threadEntryRepository.GetByThreadId(threadId, pageSize, skip);
                var returningDtos = _mapper.Map<IEnumerable<ThreadEntryDto>>(entries);
                return new ServiceResponse<IEnumerable<ThreadEntryDto>>(ServiceStatus.Ok).SetData(returningDtos);
            } catch (Exception ex)
            {
                return new ServiceResponse<IEnumerable<ThreadEntryDto>>(ServiceStatus.Error).SetException(ex);
            }
        }
    }
}
