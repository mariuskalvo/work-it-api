using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Core.DTOs;
using Core.Entities;
using Core.Services.Interfaces;
using Core.DataAccess;

namespace Core.Services
{
    public class ThreadEntryService : IThreadEntryService
    {
        private readonly AppDbContext context;
        private readonly IMapper mapper;

        public ThreadEntryService(AppDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public ThreadEntryDto Create(CreateThreadEntryDto createDto)
        {
            var mappedEntity = mapper.Map<ThreadEntry>(createDto);

            mappedEntity.CreatedAt = DateTime.Now;
            context.ThreadEntries.Add(mappedEntity);
            context.SaveChanges();

            return mapper.Map<ThreadEntryDto>(mappedEntity);
        }

        public IEnumerable<ThreadEntryDto> GetByThreadId(long threadId)
        {
            var entities = context.ThreadEntries.Where(e => e.GroupThreadId == threadId).ToList();
            return mapper.Map<IEnumerable<ThreadEntryDto>>(entities);
        }
    }
}
