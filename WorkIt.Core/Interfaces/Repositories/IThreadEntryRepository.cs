﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WorkIt.Core.Entities;

namespace WorkIt.Core.Interfaces.Repositories
{
    public interface IThreadEntryRepository
    {
        Task<ThreadEntry> Create(ThreadEntry threadEntry);
        Task<IEnumerable<ThreadEntry>> GetByThreadId(long threadId, int take, int skip);
    }
}
