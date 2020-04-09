namespace BugTracker.Models
{
    public interface IBugPriorty
    {
        int Id { get; set; }
        int Priority { get; set; }
        string Name { get; set; }
    }
}