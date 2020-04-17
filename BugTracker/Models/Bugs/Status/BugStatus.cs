namespace BugTracker.Models.Bugs.Status
{
    public class BugStatus : IBugStatus
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
