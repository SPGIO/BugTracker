using BugTracker.Models.Bugs.Priority;
using BugTracker.Models.Bugs.Status;
using System;

namespace BugTracker.Models.Bugs
{
    public interface IBug
    {
        int Id { get; set; }
        string HowToReproduceBug { get; set; }
        DateTime DateReported { get; set; }
        DateTime DateFixed { get; set; }
        BugStatus Status { get; set; }
        BugPriority Priorty { get; set; }
    }
}
