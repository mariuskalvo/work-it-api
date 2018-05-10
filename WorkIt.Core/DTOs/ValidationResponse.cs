using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DTOs
{
    public class ValidationResponse
    {
        public IEnumerable<string> Errors { get; set; }
        public bool Success { get; set; }
    }
}
