using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Core.DTOs;

namespace Core.Services.Interfaces
{
    public interface IThreadEntryService
    {
        ThreadEntryDto Create(CreateThreadEntryDto createDto, string currentUserId);
        IEnumerable<ThreadEntryDto> GetByThreadId(long threadId);
        void AddReactionToThreadEntry(AddEntryReactionDto addReactionDto, string currentUserId);
    }
}
