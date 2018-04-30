using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Core.DTOs;

namespace Core.Services.Interfaces
{
    public interface IThreadEntryService
    {
        ThreadEntryDto Create(CreateThreadEntryDto createDto);
        IEnumerable<ThreadEntryDto> GetByThreadId(long threadId);
    }
}
