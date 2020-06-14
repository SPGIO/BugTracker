using BugTracker.Models.Users;

namespace BugTracker.Models.DomainModels
{
    public class UserProjects
    {
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }
    }
}
