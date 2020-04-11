using BugTracker.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BugTracker.Models.Repositories.Projects
{
    public class ProjectRepository : IRepository<Project>
    {

        public ApplicationDbContext Context { get; set; }
        public ProjectRepository(ApplicationDbContext context)
            => Context = context;

        public int Add(Project item)
        {
            if (item == null) throw new ArgumentNullException();
            Context.Add(item);
            return item.Id;
        }

        public bool Delete(Project item)
        {
            if (item == null) return false;
            Context.Remove(item);
            return true;
        }

        public async Task<IEnumerable<Project>> GetAll()
        {
            var project = Context.Projects;
            var bugsInProject = project.Include(project => project.Bugs);
            bugsInProject.ThenInclude(bug => bug.FixedBy);
            bugsInProject.ThenInclude(bug => bug.ReportedBy);
            bugsInProject.ThenInclude(bug => bug.Status);
            bugsInProject.ThenInclude(bug => bug.Severity);
            project.Include(project => project.Team);
            return await project.ToListAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            try
            {
                await Context.SaveChangesAsync();
                return true;
            }
            catch (Exception exception)
            {
                LogError(exception);
            }
            return false;
        }

        public async Task<Project> GetById(int id)
        {
            var projectFound =  await Context.Projects
                .FirstOrDefaultAsync(project => project.Id == id);
            if (projectFound == null) throw new KeyNotFoundException();
            return projectFound;
        }

        public bool Update(Project item)
        {
            if (item == null) return false;
            Context.Projects.Update(item);
            return true;
        }

        public void LogError(Exception exception)
            => Debug.WriteLine($"Error!\n{exception.Message}");
    }
}
