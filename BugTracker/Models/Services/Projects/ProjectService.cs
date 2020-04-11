using BugTracker.Models.Repositories.Projects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTracker.Models.Services.Projects
{
    public class ProjectService
    {
        public ProjectRepository ProjectRepository { get; }
        public ProjectService(ProjectRepository projectRepository)
            => ProjectRepository = projectRepository
            ?? throw new ArgumentNullException(nameof(projectRepository));

        public async Task<int> AddProject(Project project)
        {
            try
            {
                var projectId = ProjectRepository.Add(project);
                await ProjectRepository.SaveChangesAsync();
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

        public async Task<Project> GetProjectById(int id)
        {
            var allProjects = await GetAllProjects();
            Project projectFound = allProjects.FirstOrDefault(project => project.Id == id);
            if (projectFound == null)
                throw new ProjectNotFoundException();
            return projectFound;
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
    }


}
