using BugTracker.Models.Bugs;
using BugTracker.Models.Repositories;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BugTracker.Models.Services.Projects
{
    public interface IProjectService
    {
        IRepository<Project> Repository { get; }

        int AddProject(Project project);
        Task<int> AddProjectAsync(Project project);
        void AddUserToProject(int? projectId, ApplicationUser userToBeAdded);
        Task AddUserToProjectAsync(int? projectId, ApplicationUser userToBeAdded);
        void AddUserToTeam(Project project, ApplicationUser user);
        void DeleteProject(Project project);
        Task DeleteProjectAsync(Project project);
        IEnumerable<Project> GetAllProjects(bool asTracking = true);
        Task<IEnumerable<Project>> GetAllProjectsAsync(bool asTracking = true);
        IEnumerable<Bug> GetBugsInProjectById(int projectId, bool asTracking = true);
        Task<IEnumerable<Bug>> GetBugsInProjectByIdAsync(int projectId, bool asTracking = true);
        IEnumerable<Bug> GetBugsInProjectByName(string projectName,bool asTracking = true);
        Task<IEnumerable<Bug>> GetBugsInProjectByNameAsync(string projectName,bool asTracking = true);
        Project GetProjectById(int? id, bool asTracking = true);
        Task<Project> GetProjectByIdAsync(int? id, bool asTracking = true);
        Project GetProjectByName(string name, bool asTracking = true);
        Task<Project> GetProjectByNameAsync(string name, bool asTracking = true);
        IEnumerable<Project> GetProjectsRelatedToUser(string userId, bool asTracking = true);
        Task<IEnumerable<Project>> GetProjectsRelatedToUserAsync(string userId, bool asTracking = true);
        bool IsUserInProject(int? projectId, string userId);
        Task<bool> IsUserInProjectAsync(int? projectId, string userId);
        bool ProjectExists(int? id);
        Task<bool> ProjectExistsAsync(int? id);
        void UpdateProject(Project project);
        Task UpdateProjectAsync(Project project);
    }
}