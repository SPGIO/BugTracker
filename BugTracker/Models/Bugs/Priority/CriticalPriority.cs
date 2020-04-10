namespace BugTracker.Models.Bugs.Priority
{
    public class CriticalPriority : IBugPriority
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Priority { get; set; }
    }
}
