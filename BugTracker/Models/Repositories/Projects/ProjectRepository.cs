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


        public async Task<IEnumerable<Project>> GetAllAsync(bool asTracking = true)
        {
            var query =  Context.Projects
                           .Include(project => project.Bugs)
                               .ThenInclude(bug => bug.Severity)
                           .Include(project => project.Bugs)
                               .ThenInclude(bug => bug.Status)
                           .Include(project => project.Team)
                           .Include(project => project.Bugs)
                               .ThenInclude(bug => bug.ReportedBy);
            if (asTracking)
                return await query.AsTracking().ToListAsync();
            return await query.AsNoTracking().ToListAsync();
        }

        public IEnumerable<Project> GetAll(bool asTracking)
        {

            var query = Context.Projects
            .Include(project => project.Bugs).ThenInclude(bug => bug.Severity)
            .Include(project => project.Bugs).ThenInclude(bug => bug.Status)
            .Include(project => project.Team);
            if (asTracking)
                return query.AsTracking().ToList();
            return query.AsNoTracking().ToList();

        }

        public async Task SaveChangesAsync() => await Context.SaveChangesAsync();

        public void SaveChanges() => Context.SaveChanges();

        public async Task<Project> GetByIdAsync(int id, bool asTracking = true)
        {
            var query = Context.Projects
                               .Include(project => project.Team)
                               .Include(project => project.Bugs);
            if (asTracking)
                return await query.AsTracking().SingleOrDefaultAsync(project => project.Id == id);
            return await query.AsNoTracking().SingleOrDefaultAsync(project => project.Id == id);
        }

        public Project GetById(int id, bool asTracking = true)
        {
            var query = Context.Projects
                           .Include(project => project.Team)
                           .Include(project => project.Bugs);

            if (asTracking)
                return query.AsTracking().SingleOrDefault(project => project.Id == id);
            return query.AsNoTracking().SingleOrDefault(project => project.Id == id);

        }

        public void Update(Project item)
        {
            Context.Projects.Update(item);
        }

        public async Task<IEnumerable<Project>> GetByQueryAsync(Expression<Func<Project, bool>> query, bool asTracking)
        {
            var thisQuery = Context.Projects.Where(query)
                                                .Include(project => project.Team)
                                                .Include(project => project.Bugs)
                                                .Include(project => project.Team)
                                                .ThenInclude(userProjects => userProjects.User);
            if (asTracking)
                return await thisQuery.AsTracking().ToListAsync();
            return await thisQuery.AsNoTracking().ToListAsync();
        }

        public async Task<bool> ExistsAsync(int id)
            => await Context.Projects.AnyAsync(project
                => project.Id == id);

        public bool Exists(int id)
           => Context.Projects.Any(project
               => project.Id == id);

        public Project GetById(int id)
        {
            throw new NotImplementedException();
        }


    }
}
