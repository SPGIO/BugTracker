using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace BugTracker.Models.ViewModels.Bugs
{
    public class EditBugReportViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        [DisplayName("How to reproduce")]
        public string HowToReproduce { get; set; }
        public int SeverityId { get; set; }
        public int StatusId { get; set; }
    }
}
