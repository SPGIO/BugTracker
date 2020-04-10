namespace BugTracker.Models.Bugs
{
    public interface IBugStatus
    {
        int Id { get; set; }
        string Name { get; set; }
    }
}
