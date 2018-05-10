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

        public ThreadEntryDto Create(CreateThreadEntryDto createDto, string currentUserId)
        {
            var entityToAdd = mapper.Map<ThreadEntry>(createDto);

            entityToAdd.CreatedAt = DateTime.Now;
            entityToAdd.CreatedById = currentUserId;

            context.ThreadEntries.Add(entityToAdd);
            context.SaveChanges();

            return mapper.Map<ThreadEntryDto>(entityToAdd);
        }

        public IEnumerable<ThreadEntryDto> GetByThreadId(long threadId)
        {
            var entities = context.ThreadEntries.Where(e => e.GroupThreadId == threadId).ToList();
            return mapper.Map<IEnumerable<ThreadEntryDto>>(entities);
        }

        public void AddReactionToThreadEntry(AddEntryReactionDto addReactionDto, string currentUserId)
        {
            var threadEntryId = addReactionDto.ThreadEntryId;
            var reactionTag = addReactionDto.ReactionTag;

            var reactionExists = 
                context.ThreadEntryReactions
                       .Any(t => 
                            t.ThreadEntryId == threadEntryId &&
                            t.ReactionTag.Equals(reactionTag, StringComparison.InvariantCultureIgnoreCase)
                        );

            if (reactionExists)
                return;

            var entityToAdd = mapper.Map<ThreadEntryReaction>(addReactionDto);
            entityToAdd.CreatedById = currentUserId;

            context.ThreadEntryReactions.Add(entityToAdd);
            context.SaveChanges();

        }
    }
}
