using BugTracker.Data;
using BugTracker.Models.Bugs.Severity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BugTracker.Models.Repositories.Severity
{
    public class SeverityRepository : IRepository<BugSeverity>
    {
        public SeverityRepository(ApplicationDbContext context)
            => Context = context;

        public ApplicationDbContext Context { get; set; }

        public void Add(BugSeverity item) => Context.Add(item);

        public async Task AddAsync(BugSeverity item)
            => await Context.AddAsync(item);

        public void Delete(BugSeverity item) => Context.Remove(item);

        public bool Exists(int id)
            => Context.BugSeverities.Any(severity => severity.Id == id);

        public async Task<bool> ExistsAsync(int id)
            => await Context.BugSeverities
                .AnyAsync(severity => severity.Id == id);

        
        public IEnumerable<BugSeverity> GetAll(bool asTracking = true)
            => asTracking
            ? Context.BugSeverities.AsTracking().ToList()
            : Context.BugSeverities.AsNoTracking().ToList();

        public async Task<IEnumerable<BugSeverity>> GetAllAsync(bool asTracking = true)
            => asTracking
            ? await Context.BugSeverities.AsTracking().ToListAsync()
            : await Context.BugSeverities.AsNoTracking().ToListAsync();


       

        public BugSeverity GetById(int id, bool asTracking = true)
            => asTracking
            ? Context.BugSeverities.AsTracking()
                                 .SingleOrDefault(severity => severity.Id == id)
            : Context.BugSeverities.AsNoTracking()
                                 .SingleOrDefault(severity => severity.Id == id);

        public async Task<BugSeverity> GetByIdAsync(int id, bool asTracking = true)
            => asTracking
            ? await Context.BugSeverities.AsTracking()
                                 .SingleOrDefaultAsync(severity => severity.Id == id)
            : await Context.BugSeverities.AsNoTracking()
                                 .SingleOrDefaultAsync(severity => severity.Id == id);



        public async Task<IEnumerable<BugSeverity>> GetByQueryAsync(Expression<Func<BugSeverity, bool>> query, bool asTracking = true)
            => asTracking
            ? await Context.BugSeverities.Where(query)
                                         .AsTracking()
                                         .ToListAsync()
            : await Context.BugSeverities.Where(query)
                                         .AsNoTracking()
                                         .ToListAsync();

        public void SaveChanges() => Context.SaveChanges();

        public async Task SaveChangesAsync()
            => await Context.SaveChangesAsync();

        public void Update(BugSeverity item) => Context.Update(item);
    }
}