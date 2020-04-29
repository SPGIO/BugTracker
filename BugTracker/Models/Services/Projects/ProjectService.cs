using BugTracker.Data;
using BugTracker.Models.Bugs;
using BugTracker.Models.Repositories;
using BugTracker.Models.Repositories.Projects;
using BugTracker.Models.Repositories.Users;
using BugTracker.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BugTracker.Models.Services.Projects
{
    public class ProjectService : IProjectService
    {
        public IRepository<Project> Repository { get; }
        public IUserRepository UserRepository { get; }

        public ProjectService(ApplicationDbContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));
            Repository = new ProjectRepository(context);
        }

        public async Task<int> AddProjectAsync(Project project)
        {
            try
            {
                await Repository.AddAsync(project);
                await Repository.SaveChangesAsync();
                int projectId = project.Id;
                return projectId;
            }
            catch (ArgumentNullException)
            {
                throw new ProjectNotAddedException();
            }
        }

        public int AddProject(Project project)
        {
            try
            {
                Repository.Add(project);
                Repository.SaveChanges();
                int projectId = project.Id;
                return projectId;
            }
            catch (ArgumentNullException)
            {
                throw new ProjectNotAddedException();
            }
        }

        public async Task<Project> GetProjectByNameAsync(string name)
        {
            if (string.IsNullOrEmpty(name)) throw new ProjectNotFoundException();
            Expression<Func<Project, bool>> expression = project => project.Name == name;
            var projects = await Repository.GetByQueryAsync(expression);
            var project = projects.FirstOrDefault();
            if (project == null) throw new ProjectNotFoundException();
            return project;
        }

        public Project GetProjectByName(string name)
        {
            if (string.IsNullOrEmpty(name)) throw new ProjectNotFoundException();
            var allProjects = Repository.GetAll();
            var project = allProjects.FirstOrDefault(project => project.Name == name);
            if (project == null) throw new ProjectNotFoundException();
            return project;
        }

        public void UpdateProject(Project project)
        {
            if (project == null) throw new ProjectNotFoundException();
            Repository.Update(project);
            Repository.SaveChanges();
        }

        public async Task AddBugReportToProject(int projectId, Bug bugReport)
        {
            if (!await Repository.ExistsAsync(projectId))
            {
                throw new ProjectNotFoundException();
            }

            var project = await Repository.getEditableByIdAsync(projectId);
            project.Bugs.Add(bugReport);
            await UpdateProjectAsync(project);
        }

        public async Task UpdateProjectAsync(Project project)
        {
            if (project == null) throw new ProjectNotFoundException();
            Repository.Update(project);
            await Repository.SaveChangesAsync();
        }

        public async Task<Project> GetProjectByIdAsync(int? id)
        {
            if (!id.HasValue) throw new ProjectNotFoundException();
            var projectFound = await Repository.GetByIdAsync(id.Value);
            if (projectFound == null) throw new ProjectNotFoundException();
            return projectFound;
        }

        public Project GetProjectById(int? id)
        {
            if (!id.HasValue) throw new ProjectNotFoundException();
            var projectFound = Repository.GetById(id.Value);
            if (projectFound == null) throw new ProjectNotFoundException();
            Repository.SaveChanges();
            return projectFound;
        }

        public async Task DeleteProjectAsync(Project project)
        {
            if (project == null) throw new ProjectNotFoundException();
            Repository.Delete(project);
            await Repository.SaveChangesAsync();
        }
        public void DeleteProject(Project project)
        {
            if (project == null) throw new ProjectNotFoundException();
            Repository.Delete(project);
            Repository.SaveChanges();
        }


        public async Task<IEnumerable<Project>> GetAllProjectsAsync()
        {
            var allProjects = await Repository.GetAllAsync() ?? new List<Project>();
            Repository.SaveChanges();
            return allProjects;
        }

        public IEnumerable<Project> GetAllProjects()
        {
            var allproject = Repository.GetAll() ?? new List<Project>();
            Repository.SaveChanges();
            return allproject;
        }

        public async Task<bool> ProjectExistsAsync(int? id)
        {
            if (id == null) throw new ArgumentNullException(nameof(IProject));
            return await Repository.ExistsAsync(id.Value);
        }

        public bool ProjectExists(int? id)
        {
            if (id == null) throw new ArgumentNullException(nameof(IProject));
            return Repository.Exists(id.Value);
        }

        public async Task AddUserToProjectAsync(int? projectId, string userId)
        {
            if (projectId != null && await ProjectExistsAsync(projectId.Value))
            {
                var project = await GetProjectByIdAsync(projectId);
                var user = await UserRepository.GetById(userId);
                AddUserToTeam(project, user);
                Repository.Update(project);
                await Repository.SaveChangesAsync();
            }
            else
            {
                throw new ProjectNotFoundException();
            }
        }

        public void AddUserToProject(int? projectId, string userId)
        {
            if (projectId == null || !ProjectExists(projectId.Value))
                throw new ProjectNotFoundException();

            var project = GetProjectById(projectId);
            var user = UserRepository.GetById(userId).Result;
            AddUserToTeam(project, user);
            Repository.Update(project);
            Repository.SaveChanges();
        }

        public void AddUserToTeam(Project project, ApplicationUser user)
            => project.Team.Add(new UserProjects()
            {
                UserId = user.Id,
                User = user,
                ProjectId = project.Id,
                Project = project
            });

        public async Task<bool> IsUserInProjectAsync(int? projectId, string userId)
        {
            if (projectId == null) throw new ArgumentNullException("projectId cannot be null");
            var project = await GetProjectByIdAsync(projectId.Value);
            if (project == null) throw new ProjectNotFoundException();
            var isUserInProject = project.Team.Any(userProject => userProject.UserId == userId);
            return isUserInProject;
        }

        public bool IsUserInProject(int? projectId, string userId)
        {
            if (projectId == null) throw new ArgumentNullException("projectId cannot be null");
            var project = GetProjectById(projectId.Value);
            if (project == null) throw new ProjectNotFoundException();
            var isUserInProject = project.Team.Any(userProject => userProject.UserId == userId);
            return isUserInProject;
        }


        public async Task<IEnumerable<Project>> GetProjectsRelatedToUserAsync(string userId)
        {
            var allProjects = await GetAllProjectsAsync();
            var projectsRelatedToUser = allProjects
                .Where(project => project.Team.Any(userProject => userProject.UserId == userId));
            if (projectsRelatedToUser == null) return new List<Project>();
            return projectsRelatedToUser;
        }

        public IEnumerable<Project> GetProjectsRelatedToUser(string userId)
        {
            var allProjects =  Repository.GetAll();
            var projectsRelatedToUser = allProjects
                .Where(project => project.Team.Any(userProject => userProject.UserId == userId));
            if (projectsRelatedToUser == null) return new List<Project>();
            return projectsRelatedToUser;
        }

        public async Task<IEnumerable<Bug>> GetBugsInProjectByIdAsync(int projectId)
        {
            var project = await GetProjectByIdAsync(projectId);
            if (project == null) throw new ProjectNotFoundException();
            return project.Bugs;
        }

        public IEnumerable<Bug> GetBugsInProjectById(int projectId)
        {
            var project = GetProjectById(projectId);
            if (project == null) throw new ProjectNotFoundException();
            return project.Bugs;
        }

        public IEnumerable<Bug> GetBugsInProjectByName(string projectName)
        {
            var allProjects = GetAllProjects();
            var project = allProjects.First(project => project.Name == projectName);
            if (project == null) throw new ProjectNotFoundException();
            return project.Bugs;
        }

        public async Task<IEnumerable<Bug>> GetBugsInProjectByNameAsync(string projectName)
        {
            var allProjects = await GetAllProjectsAsync();
            var project = allProjects.FirstOrDefault(project => project.Name == projectName);
            if (project == null) throw new ProjectNotFoundException();
            return project.Bugs;
        }
    }
}

