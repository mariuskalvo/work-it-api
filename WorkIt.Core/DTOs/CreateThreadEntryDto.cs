using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DTOs
{
    public class CreateThreadEntryDto
    {
        public long GroupThreadId { get; set; }
        public string Content { get; set; }
    }
}
