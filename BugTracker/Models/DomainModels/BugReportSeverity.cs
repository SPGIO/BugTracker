namespace BugTracker.Models.DomainModels
{
    public class BugReportSeverity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Priority { get; set; }
        public string Color { get; set; }
    }
}
