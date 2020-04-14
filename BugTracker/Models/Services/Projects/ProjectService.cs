using BugTracker.Models.Bugs;
using BugTracker.Models.Repositories.Projects;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTracker.Models.Services.Projects
{
    public class ProjectService
    {
        public IProjectRepository ProjectRepository { get; }
        public ProjectService(IProjectRepository projectRepository)
            => ProjectRepository = projectRepository
            ?? throw new ArgumentNullException(nameof(projectRepository));

        public async Task<int> AddProject(Project project)
        {
            try
            {
                var projectId = ProjectRepository.Add(project);
                await ProjectRepository.SaveChangesAsync();
                projectId = project.Id;
                return projectId;
            }
            catch (ArgumentNullException)
            {
                throw new ProjectNotAddedException();
            }
        }


        public async Task<bool> UpdateProject(Project project)
        {
            var isModified =  ProjectRepository.Update(project);
            if (isModified)
                await ProjectRepository.SaveChangesAsync();
            return isModified;
        }

        public async Task<Project> GetProjectById(int? id)
        {
            try
            {
                Project projectFound = await ProjectRepository.GetById(id);
                return projectFound;
            }
            catch (Exception)
            {
                throw new ProjectNotFoundException();
            }
        }

        public async Task<bool> DeleteProject(Project project)
        {
            var isDeleted =  ProjectRepository.Delete(project);
            if (isDeleted)
                await ProjectRepository.SaveChangesAsync();
            return isDeleted;
        }

        public async Task<IEnumerable<Project>> GetAllProjects()
        => await ProjectRepository.GetAll();

        public async Task<bool> ProjectExists(int id)
        {
            return await ProjectRepository.Exists(id);
        }

        public async Task<bool> AddTeamMember(int projectId, string userId)
        {
            // --------------------------------------------------------------
            // Needs refactoring High level abstraction should not depend on
            // low level abstraction!
            var userToBeAdded = await ProjectRepository.Context.Users
                .FirstOrDefaultAsync(user => user.Id == userId);
            // --------------------------------------------------------------
            if (userToBeAdded == null) return false;
            
            var projectExists = await ProjectRepository.Exists(projectId);
            if (!projectExists) return false;

            var project = await ProjectRepository.GetById(projectId);
            if (project.Team == null)
                project.Team = new List<IdentityUser>();
            project.Team.Add(userToBeAdded);
            var updateSucceeded = ProjectRepository.Update(project);
            await ProjectRepository.SaveChangesAsync();
            return updateSucceeded;
        }


        public async Task<bool> IsUserInProject(int projectId, string userId)
        {
            var project = await GetProjectById(projectId);
            var isUserInProject = project.Team.Any(user => user.Id == userId);
            return isUserInProject;
        }

        
        public async Task<IEnumerable<Project>> GetProjectsRelatedToUser(string userId)
        {
            var allProjects = await ProjectRepository.GetAll();
            var projectsWithTeams = allProjects.Where(project => project.Team != null);
            var projectsRelatedToUser = projectsWithTeams.Where(project => project.Team.Any(user => user.Id == userId));
            return projectsRelatedToUser;
        }

        public async Task<IEnumerable<Bug>> GetBugsInProjectById(int projectId)
        {
            var project = await GetProjectById(projectId);
            return project.Bugs;
        }
        public async Task<IEnumerable<Bug>> GetBugsInProjectByName(string projectName)
        {
            var allProjects = await GetAllProjects();
            var project = allProjects.First(project => project.Name == projectName);
            return project.Bugs;
        }
    }
}

