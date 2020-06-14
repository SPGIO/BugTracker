using BugTracker.Models.DomainModels;

namespace BugTracker.Data.Repositories
{
    public interface IProjectRepository : IRepository<Project>
    {
        void AddUserToProject(string userId, int ProjectId);
        Project GetByName(string name);

        bool IsUserInProject(int projectId, string userId);
    }
}
