using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.Entities
{
    public class ThreadEntryReaction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public ThreadEntry ThreadEntry { get; set; }
        public long ThreadEntryId { get; set; }
        public string ReactionTag { get; set; }
    }
}
