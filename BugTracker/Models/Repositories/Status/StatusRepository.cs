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

 

        public IEnumerable<BugStatus> GetAll()
            => Context.BugStatuses.AsNoTracking().ToList();


        public async Task<IEnumerable<BugStatus>> GetAllAsync()
            => await Context.BugStatuses.AsNoTracking().ToListAsync();

        public BugStatus GetById(int id)
            => Context.BugStatuses.AsNoTracking()
                                 .SingleOrDefault(Status => Status.Id == id);

        public async Task<BugStatus> GetByIdAsync(int id)
            => await Context.BugStatuses
                                .AsNoTracking()
                                .SingleOrDefaultAsync(Status => Status.Id == id);

        public async Task<IEnumerable<BugStatus>> GetByQueryAsync(Expression<Func<BugStatus, bool>> query)
            => await Context.BugStatuses.Where(query)
                                       .AsNoTracking()
                                       .ToListAsync();

        public BugStatus GetEditableById(int id) 
            => Context.BugStatuses.AsTracking()
            .SingleOrDefault(Status => Status.Id == id);

        public async Task<BugStatus> getEditableByIdAsync(int id)
         => await Context.BugStatuses.AsTracking()
                    .SingleOrDefaultAsync(Status => Status.Id == id);

        public void SaveChanges() => Context.SaveChanges();

        public async Task SaveChangesAsync()
            => await Context.SaveChangesAsync();

        public void Update(BugStatus item) => Context.Update(item);
    }
}
