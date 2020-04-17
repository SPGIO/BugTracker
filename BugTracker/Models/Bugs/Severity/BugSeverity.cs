namespace BugTracker.Models.Bugs.Severity
{
    public class BugSeverity : IBugSeverity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Priority { get; set; }
    }
}
