using System;

namespace BugTracker.Models.ViewModels.Bugs
{
    public class BugDetailViewModel
    {
        public string Severity { get; set; }
        public string Createdby { get; set; }
        public string HowToReproduce { get; set; }
        public DateTime DateReported { get; set; }
    }
}
