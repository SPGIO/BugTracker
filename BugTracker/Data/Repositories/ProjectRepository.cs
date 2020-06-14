using BugTracker.Models.DomainModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BugTracker.Data.Repositories
{
    public class ProjectRepository : Repository<Project>, IProjectRepository
    {

        public ApplicationDbContext ApplicationDbContext =>
            context as ApplicationDbContext;
        public ProjectRepository(DbContext context) : base(context) { }

        public void AddUserToProject(string userId, int ProjectId)
        {
            var userProject = new UserProjects
            {
                ProjectId = ProjectId,
                UserId = userId
            };
            ApplicationDbContext.UserProjects.Add(userProject);
        }


        public Project GetByName(string name) => 
            ApplicationDbContext.Projects.FirstOrDefault(project => project.Name == name);

        public bool IsUserInProject(int projectId, string userId) =>
            ApplicationDbContext.UserProjects.Any(userproject =>
                userproject.ProjectId == projectId &&
                userproject.UserId == userId);
       











        public async Task AddAsync(Project item)
        {
            await ApplicationDbContext.AddAsync(item);
        }

        public void Add(Project item) => ApplicationDbContext.Add(item);

        public void Delete(Project item) => ApplicationDbContext.Remove(item);


        public async Task<IEnumerable<Project>> GetAllAsync()
            => await ApplicationDbContext.Projects
            .Include(project => project.Bugs).ThenInclude(bug => bug.Severity)
            .Include(project => project.Bugs).ThenInclude(bug => bug.Status)
            .Include(project => project.Bugs).ThenInclude(bug => bug.ReportedBy)
            .Include(project => project.Team).AsNoTracking().ToListAsync();

        public IEnumerable<Project> GetAll() => ApplicationDbContext.Projects
            .Include(project => project.Bugs).ThenInclude(bug => bug.Severity)
            .Include(project => project.Bugs).ThenInclude(bug => bug.Status)
            .Include(project => project.Team).AsNoTracking().ToList();

        public async Task SaveChangesAsync() => await ApplicationDbContext.SaveChangesAsync();

        public void SaveChanges() => ApplicationDbContext.SaveChanges();

        public async Task<Project> GetByIdAsync(int id)
            => await ApplicationDbContext.Projects.Include(project => project.Team)
                                     .Include(project => project.Bugs)
                                     .Include(project => project.Team)
                                     .ThenInclude(userProjects => userProjects.User)
                                     .AsNoTracking()
                                     .SingleOrDefaultAsync(project => project.Id == id);

        public Project GetById(int id) =>
            ApplicationDbContext.Projects.Include(project => project.Team)
                            .Include(project => project.Bugs)
                            .Include(project => project.Team)
                            .ThenInclude(userProjects => userProjects.User)
                            .AsNoTracking()
                            .SingleOrDefault(project => project.Id == id);

        public void Update(Project item)
        {
            ApplicationDbContext.Projects.Update(item);
        }

        public async Task<IEnumerable<Project>> GetByQueryAsync(Expression<Func<Project, bool>> query)
            => await ApplicationDbContext.Projects.Where(query)
                                     .Include(project => project.Team)
                                     .Include(project => project.Bugs)
                                     .Include(project => project.Team)
                                     .ThenInclude(userProjects => userProjects.User)
                                     .AsNoTracking()
                                     .ToListAsync();

        public async Task<bool> ExistsAsync(int id)
            => await ApplicationDbContext.Projects.AnyAsync(project
                => project.Id == id);

        public bool Exists(int id)
           => ApplicationDbContext.Projects.Any(project
               => project.Id == id);

        public Project GetEditableById(int id)
            => ApplicationDbContext.Projects.Include(project => project.Team)
                               .Include(project => project.Bugs)
                               .Include(project => project.Team)
                               .ThenInclude(userProjects => userProjects.User)
                               .AsTracking()
                               .SingleOrDefault(project => project.Id == id);


        public async Task<Project> getEditableByIdAsync(int id)
            => await ApplicationDbContext.Projects.Include(project => project.Team)
                               .Include(project => project.Bugs)
                               .Include(project => project.Team)
                               .ThenInclude(userProjects => userProjects.User)
                               .AsTracking()
                               .SingleOrDefaultAsync(project => project.Id == id);
    }
}
