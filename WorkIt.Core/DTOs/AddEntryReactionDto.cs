using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DTOs
{
    public class AddEntryReactionDto
    {
        public long ThreadEntryId { get; set; }
        public string ReactionTag { get; set; }
    }
}
