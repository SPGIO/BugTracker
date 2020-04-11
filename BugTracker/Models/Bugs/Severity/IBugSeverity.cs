namespace BugTracker.Models.Bugs.Severity
{
    public interface IBugSeverity
    {
        int Id { get; set; }
        int Priority { get; set; }
        string Name { get; set; }
    }
}