using BugTracker.Models.Bugs;
using BugTracker.Models.Repositories;
using Microsoft.AspNetCore.Identity;
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

        public ProjectService(IRepository<Project> repository)
            => Repository = repository
            ?? throw new ArgumentNullException(nameof(repository));


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

        public async Task<Project> GetProjectByNameAsync(string name, bool asTracking = true)
        {
            if (string.IsNullOrEmpty(name)) throw new ProjectNotFoundException();
            var projects = await Repository.GetAllAsync(asTracking);
            var project = projects.FirstOrDefault(project => project.Name == name);
            if (project == null) throw new ProjectNotFoundException();
            return project;
        }

        public Project GetProjectByName(string name, bool asTracking = true)
        {
            if (string.IsNullOrEmpty(name)) throw new ProjectNotFoundException();
            var allProjects = Repository.GetAll(asTracking);
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

        public async Task UpdateProjectAsync(Project project)
        {
            if (project == null) throw new ProjectNotFoundException();
            Repository.Update(project);
            await Repository.SaveChangesAsync();
        }

        public async Task<Project> GetProjectByIdAsync(int? id, bool asTracking = true)
        {
            if (!id.HasValue) throw new ProjectNotFoundException();
            var projectFound = await Repository.GetByIdAsync(id.Value, asTracking);
            if (projectFound == null) throw new ProjectNotFoundException();
            return projectFound;
        }

        public Project GetProjectById(int? id, bool asTracking = true)
        {
            if (!id.HasValue) throw new ProjectNotFoundException();
            var projectFound = Repository.GetById(id.Value, asTracking);
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


        public async Task<IEnumerable<Project>> GetAllProjectsAsync(bool asTracking = true)
        {
            var allProjects = await Repository.GetAllAsync(asTracking) ?? new List<Project>();
            Repository.SaveChanges();
            return allProjects;
        }

        public IEnumerable<Project> GetAllProjects(bool asTracking = true)
        {
            var allproject = Repository.GetAll(asTracking) ?? new List<Project>();
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

        public async Task AddUserToProjectAsync(int? projectId, ApplicationUser userToBeAdded)
        {
            if (projectId == null) throw new ProjectNotFoundException();
            var projectExists = await ProjectExistsAsync(projectId.Value);
            if (!projectExists) throw new ProjectNotFoundException();
            bool tracking = true;
            var project = await GetProjectByIdAsync(projectId, tracking);
            AddUserToTeam(project, userToBeAdded);
            Repository.Update(project);
            await Repository.SaveChangesAsync();
        }

        public void AddUserToProject(int? projectId, ApplicationUser userToBeAdded)
        {
            if (userToBeAdded == null) throw new ArgumentNullException(nameof(ApplicationUser));
            if (projectId == null) throw new ProjectNotFoundException();
            var projectExists = ProjectExists(projectId.Value);
            if (!projectExists) throw new ProjectNotFoundException();
            bool tracking = false;
            var project = GetProjectById(projectId, tracking);
            AddUserToTeam(project, userToBeAdded);
            Repository.Update(project);
            Repository.SaveChanges();
        }

        public void AddUserToTeam(Project project, ApplicationUser user)
        {
            if (project.Team == null) project.Team = new List<UserProjects>();
            project.Team.Add(new UserProjects()
            {
                UserId = user.Id,
                User = user,
                ProjectId = project.Id,
                Project = project
            });

        }

        public async Task<bool> IsUserInProjectAsync(int? projectId, string userId)
        {
            if (projectId == null) throw new ArgumentNullException("projectId cannot be null");
            bool tracking = false;
            var project = await GetProjectByIdAsync(projectId.Value, tracking);
            if (project == null) throw new ProjectNotFoundException();
            //var isUserInProject = project.Team.Any(user => user.Id == userId);
            var isUserInProject = project.Team.Any(userProject => userProject.UserId == userId);
            return isUserInProject;
        }

        public bool IsUserInProject(int? projectId, string userId)
        {
            if (projectId == null) throw new ArgumentNullException("projectId cannot be null");
            bool tracking = false;
            var project = GetProjectById(projectId.Value, tracking);
            if (project == null) throw new ProjectNotFoundException();
            var isUserInProject = project.Team.Any(userProject => userProject.UserId == userId);
            return isUserInProject;
        }


        public async Task<IEnumerable<Project>> GetProjectsRelatedToUserAsync(string userId, bool asTracking = true)
        {
            var allProjects = await GetAllProjectsAsync(asTracking);
            var projectsRelatedToUser = allProjects
                .Where(project => project.Team.Any(userProject => userProject.UserId == userId));
            //.Where(project => project.Team.Any(user => user.Id == userId));
            if (projectsRelatedToUser == null) return new List<Project>();
            return projectsRelatedToUser;
        }

        public IEnumerable<Project> GetProjectsRelatedToUser(string userId, bool asTracking = true)
        {
            var allProjects =  Repository.GetAll(asTracking);
            var projectsRelatedToUser = allProjects
                .Where(project => project.Team.Any(userProject => userProject.UserId == userId));
            //.Where(project => project.Team.Any(user => user.Id == userId));
            if (projectsRelatedToUser == null) return new List<Project>();
            return projectsRelatedToUser;
        }

        public async Task<IEnumerable<Bug>> GetBugsInProjectByIdAsync(int projectId, bool asTracking = true)
        {
            var project = await GetProjectByIdAsync(projectId, asTracking);
            if (project == null) throw new ProjectNotFoundException();
            return project.Bugs;
        }

        public IEnumerable<Bug> GetBugsInProjectById(int projectId, bool asTracking = true)
        {
            var project = GetProjectById(projectId, asTracking);
            if (project == null) throw new ProjectNotFoundException();
            return project.Bugs;
        }

        public IEnumerable<Bug> GetBugsInProjectByName(string projectName, bool asTracking = true)
        {
            var allProjects = GetAllProjects(asTracking);
            var project = allProjects.First(project => project.Name == projectName);
            if (project == null) throw new ProjectNotFoundException();
            return project.Bugs;
        }

        public async Task<IEnumerable<Bug>> GetBugsInProjectByNameAsync(string projectName, bool asTracking = true)
        {
            var allProjects = await GetAllProjectsAsync(asTracking);
            var project = allProjects.FirstOrDefault(project => project.Name == projectName);
            if (project == null) throw new ProjectNotFoundException();
            return project.Bugs;
        }
    }
}

