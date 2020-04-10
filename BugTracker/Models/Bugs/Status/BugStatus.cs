using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTracker.Models.Bugs.Status
{
    public class BugStatus : IBugStatus
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
