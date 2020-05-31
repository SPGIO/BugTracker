using BugTracker.Models.Bugs;
using BugTracker.Models.Repositories;
using BugTracker.Models.Users;
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
        void AddUserToProject(int? projectId, string userId);
        Task AddUserToProjectAsync(int? projectId, string userId);
        void AddUserToTeam(Project project, ApplicationUser user);
        void DeleteProject(Project project);
        Task DeleteProjectAsync(Project project);
        IEnumerable<Project> GetAllProjects();
        Task<IEnumerable<Project>> GetAllProjectsAsync();
        IEnumerable<Bug> GetBugsInProjectById(int projectId);
        Task<IEnumerable<Bug>> GetBugsInProjectByIdAsync(int projectId);
        IEnumerable<Bug> GetBugsInProjectByName(string projectName);
        Task<IEnumerable<Bug>> GetBugsInProjectByNameAsync(string projectName);
        Project GetProjectById(int? id);
        Task<Project> GetProjectByIdAsync(int? id);
        Project GetProjectByName(string name);
        Task<Project> GetProjectByNameAsync(string name);
        IEnumerable<Project> GetProjectsRelatedToUser(string userId);
        Task<IEnumerable<Project>> GetProjectsRelatedToUserAsync(string userId);
        bool IsUserInProject(int? projectId, string userId);
        Task<bool> IsUserInProjectAsync(int? projectId, string userId);
        bool ProjectExists(int? id);
        Task<bool> ProjectExistsAsync(int? id);
        void UpdateProject(Project project);
        Task UpdateProjectAsync(Project project);
        Task AddBugReportToProject(int projectId, Bug bugReport);
    }
}