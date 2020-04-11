using BugTracker.Models.Bugs.Severity;
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
        public BugSeverity Severity { get; set; }
        public ApplicationUser ReportedBy { get; set; }
        public ApplicationUser FixedBy { get; set; }
    }
}
