using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WorkIt.Core.Entities
{
    public class ProjectThread
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string Title { get; set; }

        public long ProjectId { get; set; }
        public Project Project { get; set; }

        public IEnumerable<ThreadEntry> Entries { get; set; }
        public string CreatedById { get; set; }
        public ApplicationUser CreatedBy { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
