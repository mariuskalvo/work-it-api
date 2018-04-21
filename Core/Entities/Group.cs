using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities
{
    public class Group
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
