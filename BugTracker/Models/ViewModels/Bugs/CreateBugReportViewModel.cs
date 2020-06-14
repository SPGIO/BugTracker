using BugTracker.Models.DomainModels;

namespace BugTracker.Models.ViewModels.Bugs
{

    public class CreateBugReportViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string HowToReproduce { get; set; }
        public int SeverityId { get; set; }
        public ApplicationUser AssignedTo { get; set; }
    }
}
