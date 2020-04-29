using BugTracker.Models.Bugs.Severity;
using BugTracker.Models.Bugs.Status;
using BugTracker.Models.Users;
using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace BugTracker.Models.Bugs
{
    public class Bug : IBug
    {
        public int Id { get; set; }
        [Required]
        public string Title
        {
            get => title;
            set => title = value ?? string.Empty;
        }
        public string Description
        {
            get => description;
            set => description = value ?? string.Empty;
        }
        public string HowToReproduceBug
        {
            get => howToReproduceBug;
            set => howToReproduceBug = value ?? string.Empty;
        }
        [Required]
        public DateTime DateReported { get; set; }
        public DateTime? DateFixed { get; set; }
        public BugStatus Status
        {
            get => status ?? nothingSelectedStatus;
            set => status = value ?? nothingSelectedStatus;
        }
        public BugSeverity Severity
        {
            get => severity ?? nothingSelectedSeverity;
            set => severity = value ?? nothingSelectedSeverity;
        }

        public ApplicationUser? ReportedBy { get; set; }
        public ApplicationUser? FixedBy { get; set; }

        private int NumberOfDaysUntilOld = 2;
        private string title;
        private string description;
        private string howToReproduceBug;
        private BugStatus? status;
        private BugSeverity? severity;
        private readonly BugStatus nothingSelectedStatus =  new BugStatus()
        {
            Id = -1,
            Name = "Nothing selected",
            Priority = -1,
            Color = "#fff"
        };

        private readonly BugSeverity nothingSelectedSeverity =  new BugSeverity()
        {
            Id = -1,
            Name = "Nothing selected",
            Priority = -1,
            Color = "#fff"
        };


        public Bug()
        {
            DateReported = DateTime.Now;
            title = string.Empty;
            description = string.Empty;
            howToReproduceBug = string.Empty;
            Status = nothingSelectedStatus;
            Severity = nothingSelectedSeverity;

        }

        public int GetDaysFromToday(DateTime date)
            => (int) DateTime.Now.Subtract(date).TotalDays;

        public bool IsNew()
            => (GetDaysFromToday(DateReported) < NumberOfDaysUntilOld);
    }
}
