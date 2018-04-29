using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.Entities
{
   public  class ThreadEntry
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public long GroupThreadId { get; set; }
        public GroupThread Thread { get; set; }

        public string Content { get; set; }
        public IEnumerable<ThreadEntryReaction> Reactions { get; set; }

        public DateTime CreatedAt { get; set; }

    }
}
