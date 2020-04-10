using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTracker.Models.Bugs.Priority
{
    public class BugPriority : IBugPriority
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Priority { get; set; }
    }
}
