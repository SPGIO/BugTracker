using BugTracker.Models.Bugs.Priority;
using System;

namespace BugTracker.Models.Bugs
{
    public interface IBug
    {
        int Id { get; set; }
        string HowToReproduceBug { get; set; }
        DateTime DateReported { get; set; }
        DateTime DateFixed { get; set; }
        IBugStatus Status { get; set; }
        IBugPriority Priorty { get; set; }
    }
}
