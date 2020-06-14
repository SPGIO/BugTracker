namespace BugTracker.Models.Bugs.Status
{
    public interface IBugStatus
    {
        int Id { get; set; }
        string Name { get; set; }
        int Priority { get; set; }
        string Color { get; set; }
    }
}
