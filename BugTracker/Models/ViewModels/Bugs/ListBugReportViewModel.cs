using System;
using System.ComponentModel;

namespace BugTracker.Models.ViewModels.Bugs
{
    public class ListBugReportViewModel
    {

        public int Id { get; set; }
        public string Title { get; set; }
        public string SeverityName { get; set; }
        public string SeverityColor { get; set; }
        public string StatusName { get; set; }
        public string StatusColor { get; set; }
        public string Description { get; set; }
        [DisplayName("Reported By")]
        public string ReportedByName { get; set; }
        [DisplayName("Date Reported")]
        public DateTime DateReported { get; set; }
        public bool IsNew { get; set; }


    }
}
