using BugTracker.Data;
using BugTracker.Models.Bugs.Status;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BugTracker.Models.Repositories.Status
{
    public class StatusRepository : IRepository<BugStatus>
    {
        public StatusRepository(ApplicationDbContext context)
        {
            Context = context;
        }

        public ApplicationDbContext Context { get; set; }

        public void Add(BugStatus item) => Context.Add(item);

        public async Task AddAsync(BugStatus item)
            => await Context.AddAsync(item);

        public void Delete(BugStatus item) => Context.Remove(item);

        public bool Exists(int id)
            => Context.BugStatuses.Any(status => status.Id == id);

        public async Task<bool> ExistsAsync(int id)
            => await Context.BugStatuses.AnyAsync(status => status.Id == id);

 

        public IEnumerable<BugStatus> GetAll(bool asTracking = true)
            => asTracking
            ? Context.BugStatuses.AsTracking().ToList()
            : Context.BugStatuses.AsNoTracking().ToList();


        public async Task<IEnumerable<BugStatus>> GetAllAsync(bool asTracking = true)
            => asTracking
            ? await Context.BugStatuses.AsTracking().ToListAsync()
            : await Context.BugStatuses.AsNoTracking().ToListAsync();

        public BugStatus GetById(int id, bool asTracking = true)
            => asTracking 
            ? Context.BugStatuses.AsTracking()
                                 .SingleOrDefault(Status => Status.Id == id)
            : Context.BugStatuses.AsNoTracking()
                                 .SingleOrDefault(Status => Status.Id == id);

        public async Task<BugStatus> GetByIdAsync(int id, bool asTracking = true)
            => asTracking  
            ? await Context.BugStatuses
                                .AsTracking()
                                .SingleOrDefaultAsync(Status => Status.Id == id) 
            : await Context.BugStatuses
                                .AsNoTracking()
                                .SingleOrDefaultAsync(Status => Status.Id == id);

        public async Task<IEnumerable<BugStatus>> GetByQueryAsync(Expression<Func<BugStatus, bool>> query, bool asTracking = true)
            => asTracking
            ? await Context.BugStatuses.Where(query)
                                       .AsTracking()
                                       .ToListAsync()
            : await Context.BugStatuses.Where(query)
                                       .AsNoTracking()
                                       .ToListAsync();
       

        public void SaveChanges() => Context.SaveChanges();

        public async Task SaveChangesAsync()
            => await Context.SaveChangesAsync();

        public void Update(BugStatus item) => Context.Update(item);
    }
}
