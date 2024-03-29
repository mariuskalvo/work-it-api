﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WorkIt.Core.Entities;

namespace WorkIt.Core.Entities
{
    public class Project
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public bool IsOpenToJoin { get; set; }
        public bool IsPubliclyVisible { get; set; }

        public IEnumerable<ProjectThread> Threads { get; set; }
        public IEnumerable<ApplicationUserProjectMember> Members { get; set; }

        public long CreatedById { get; set; }
        public UserInfo CreatedBy { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
