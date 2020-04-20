using BugTracker.Models.Bugs.Severity;
using BugTracker.Models.Bugs.Status;
using BugTracker.Models.Users;
using Microsoft.AspNetCore.Identity;
using System;

namespace BugTracker.Models.Bugs
{
    public interface IBug
    {
        int Id { get; set; }
        string Title { get; set; }
        string Description { get; set; }
        string HowToReproduceBug { get; set; }
        ApplicationUser ReportedBy { get; set; }
        DateTime DateReported { get; set; }
        ApplicationUser FixedBy { get; set; }
        DateTime? DateFixed { get; set; }
        BugStatus Status { get; set; }
        BugSeverity Severity { get; set; }

        bool IsNew();

    }
}
