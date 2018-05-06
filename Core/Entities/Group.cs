using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.Entities
{
    public class Group
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string Title { get; set; }
        public IEnumerable<GroupThread> Threads { get; set; }
        public IEnumerable<ApplicationUserOwnedGroups> Owners { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
