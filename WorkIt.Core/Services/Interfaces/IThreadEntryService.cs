using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Core.DTOs;

namespace Core.Services.Interfaces
{
    public interface IThreadEntryService
    {
        Task<ThreadEntryDto> Create(CreateThreadEntryDto createDto, string currentUserId);
        Task<IEnumerable<ThreadEntryDto>> GetPagedByThreadId(long threadId, int page, int pageSize);
        void AddReactionToThreadEntry(AddEntryReactionDto addReactionDto, string currentUserId);
    }
}
