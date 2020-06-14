using BugTracker.Models.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BugTracker.Models.DomainModels
{
    public class BugReport
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public string HowToReproduceBug { get; set; }
        [Required]
        public DateTime DateReported { get; set; }
        public DateTime? DateFixed { get; set; }
        public BugReportStatus Status { get; set; }
        public BugReportSeverity Severity { get; set; }

        public ApplicationUser ReportedBy { get; set; }
        public ApplicationUser? FixedBy { get; set; }
        public ApplicationUser AssignedTo { get; set; }

        public bool IsClosed() => Status.Name == "Closed";


        private readonly BugReportStatus nothingSelectedStatus =  new BugReportStatus()
        {
            Id = -1,
            Name = "Nothing selected",
            Priority = -1,
            Color = "#fff"
        };

        private readonly BugReportSeverity nothingSelectedSeverity =  new BugReportSeverity()
        {
            Id = -1,
            Name = "Nothing selected",
            Priority = -1,
            Color = "#fff"
        };

        private int NumberOfDaysUntilOld = 2;

        public int GetDaysFromToday(DateTime date)
            => (int) DateTime.Now.Subtract(date).TotalDays;

        public bool IsNew()
            => (GetDaysFromToday(DateReported) < NumberOfDaysUntilOld);
    }
}
