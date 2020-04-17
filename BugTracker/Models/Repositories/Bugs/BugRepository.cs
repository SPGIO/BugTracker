using BugTracker.Data;
using BugTracker.Models.Bugs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BugTracker.Models.Repositories.Bugs
{
    public class BugRepository : IRepository<Bug>
    {
        public ApplicationDbContext Context { get; set; }
        public BugRepository(ApplicationDbContext context) => Context = context;

        public IEnumerable<Bug> GetAll(bool asTracking = true) => Context.Bugs
                .Include(bug => bug.Status)
                .Include(bug => bug.Severity)
                .Include(bug => bug.ReportedBy)
                .Include(bug => bug.FixedBy)
                .ToList();



        public async Task<IEnumerable<Bug>> GetAllAsync(bool asTracking = true)
        {
            var query = Context.Bugs.Include(bug => bug.Status)
                                    .Include(bug => bug.Severity)
                                    .Include(bug => bug.ReportedBy)
                                    .Include(bug => bug.FixedBy);
            if (asTracking)
                return await query.AsTracking().ToListAsync();
            return await query.AsNoTracking().ToListAsync();
        }

        public void Add(Bug item) => Context.Add(item);

        public async Task AddAsync(Bug item) => await Context.AddAsync(item);

        public void Update(Bug item) => Context.Update(item);

        public void Delete(Bug item) => Context.Remove(item);

        public async Task SaveChangesAsync() => await Context.SaveChangesAsync();

        public void SaveChanges() => Context.SaveChanges();



        public async Task<bool> ExistsAsync(int id)
            => await Context.Bugs.AnyAsync(bug => bug.Id == id);

        public bool Exists(int id) => Context.Bugs.Any(bug => bug.Id == id);
     

        public async Task<Bug> GetByIdAsync(int id, bool asTracking = true)
        {
            var query = Context.Bugs
                .Include(bug => bug.Status)
                .Include(bug => bug.Severity)
                .Include(bug => bug.ReportedBy)
                .Include(bug => bug.FixedBy);
            if (asTracking)
                return await query.AsTracking().FirstOrDefaultAsync(bug => bug.Id == id);
            return await query.AsNoTracking().FirstOrDefaultAsync(bug => bug.Id == id);
        }

        public async Task<IEnumerable<Bug>> GetByQueryAsync(Expression<Func<Bug, bool>> query, bool asTracking = true)
        {
            var thisQuery = Context.Bugs
                .Include(bug => bug.Status)
                .Include(bug => bug.Severity)
                .Include(bug => bug.ReportedBy)
                .Include(bug => bug.FixedBy)
                .Where(query);
            if (asTracking)
                return await thisQuery.AsTracking().ToListAsync();
            return await thisQuery.AsNoTracking().ToListAsync();
        }

        public Bug GetById(int id, bool asTracking = true)
        {
            var query = Context.Bugs
                .Include(bug => bug.Status)
                .Include(bug => bug.Severity)
                .Include(bug => bug.ReportedBy)
                .Include(bug => bug.FixedBy);
            if (asTracking)
                return query.AsTracking().FirstOrDefault(bug => bug.Id == id);
            return query.AsNoTracking().FirstOrDefault(bug => bug.Id == id);
        }
    }
}
