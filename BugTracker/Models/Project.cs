using BugTracker.Models.Bugs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTracker.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<Bug> Bugs { get; set; }
    }
}
