using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Core.DTOs;
using WorkIt.Core.DTOs;

namespace Core.Services.Interfaces
{
    public interface IThreadEntryService
    {
        Task<CrudServiceResponse<ThreadEntryDto>> Create(CreateThreadEntryDto createDto, string currentUserId);
        Task<CrudServiceResponse<IEnumerable<ThreadEntryDto>>> GetPagedByThreadId(long threadId, int page, int pageSize);
    }
}
