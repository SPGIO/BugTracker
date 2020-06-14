using BugTracker.Models.DomainModels;
using BugTracker.Models.Users;
using System.Collections.Generic;

namespace BugTracker.Data.Repositories
{
    public interface IUserRepository : IRepository<ApplicationUser>
    {
        IEnumerable<Project> GetProjectsRelatedToUser(string userId);
        ApplicationUser GetByStringId(string id);
    }
}
