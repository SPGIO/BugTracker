namespace BugTracker.Models.Bugs.Priority
{
    public interface IBugPriority
    {
        int Id { get; set; }
        int Priority { get; set; }
        string Name { get; set; }
    }
}