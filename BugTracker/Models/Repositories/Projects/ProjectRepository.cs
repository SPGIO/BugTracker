using BugTracker.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BugTracker.Models.Repositories.Projects
{
    public class ProjectRepository : IRepository<Project>
    {

        public ApplicationDbContext Context { get; set; }
        public ProjectRepository(ApplicationDbContext context)
            => Context = context;

        public async Task AddAsync(Project item)
        {
            await Context.AddAsync(item);
        }

        public void Add(Project item) => Context.Add(item);

        public void Delete(Project item) => Context.Remove(item);


        public async Task<IEnumerable<Project>> GetAllAsync()
            => await Context.Projects
            .Include(project => project.Bugs).ThenInclude(bug => bug.Severity)
            .Include(project => project.Bugs).ThenInclude(bug => bug.Status)
            .Include(project => project.Bugs).ThenInclude(bug => bug.ReportedBy)
            .Include(project => project.Team).AsNoTracking().ToListAsync();

        public IEnumerable<Project> GetAll() => Context.Projects
            .Include(project => project.Bugs).ThenInclude(bug => bug.Severity)
            .Include(project => project.Bugs).ThenInclude(bug => bug.Status)
            .Include(project => project.Team).AsNoTracking().ToList();

        public async Task SaveChangesAsync() => await Context.SaveChangesAsync();

        public void SaveChanges() => Context.SaveChanges();

        public async Task<Project> GetByIdAsync(int id)
            => await Context.Projects.Include(project => project.Team)
                                     .Include(project => project.Bugs)
                                     .Include(project => project.Team)
                                     .ThenInclude(userProjects => userProjects.User)
                                     .AsNoTracking()
                                     .SingleOrDefaultAsync(project => project.Id == id);

        public Project GetById(int id) => 
            Context.Projects.Include(project => project.Team)
                            .Include(project => project.Bugs)
                            .Include(project => project.Team)
                            .ThenInclude(userProjects => userProjects.User)
                            .AsNoTracking()
                            .SingleOrDefault(project => project.Id == id);

        public void Update(Project item)
        {
            Context.Projects.Update(item);
        }

        public async Task<IEnumerable<Project>> GetByQueryAsync(Expression<Func<Project, bool>> query)
            => await Context.Projects.Where(query)
                                     .Include(project => project.Team)
                                     .Include(project => project.Bugs)
                                     .Include(project => project.Team)
                                     .ThenInclude(userProjects => userProjects.User)
                                     .AsNoTracking()
                                     .ToListAsync();

        public async Task<bool> ExistsAsync(int id)
            => await Context.Projects.AnyAsync(project
                => project.Id == id);

        public bool Exists(int id)
           => Context.Projects.Any(project
               => project.Id == id);

        public Project GetEditableById(int id)
            => Context.Projects.Include(project => project.Team)
                               .Include(project => project.Bugs)
                               .Include(project => project.Team)
                               .ThenInclude(userProjects => userProjects.User)
                               .AsTracking()
                               .SingleOrDefault(project => project.Id == id);
        

        public async Task<Project> getEditableByIdAsync(int id)
            => await Context.Projects.Include(project => project.Team)
                               .Include(project => project.Bugs)
                               .Include(project => project.Team)
                               .ThenInclude(userProjects => userProjects.User)
                               .AsTracking()
                               .SingleOrDefaultAsync(project => project.Id == id);
    }
}
