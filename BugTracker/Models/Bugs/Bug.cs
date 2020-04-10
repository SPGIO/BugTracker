using BugTracker.Models.Bugs.Priority;
using BugTracker.Models.Bugs.Status;
using System;

namespace BugTracker.Models.Bugs
{
    public class Bug : IBug
    {
        public int Id { get; set; }
        public string HowToReproduceBug { get; set; }
        public DateTime DateReported { get; set; }
        public DateTime DateFixed { get; set; }
        public BugStatus Status { get; set; }
        public BugPriority Priorty { get; set; }
    }
}
