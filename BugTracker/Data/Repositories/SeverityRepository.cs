using BugTracker.Models.DomainModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BugTracker.Data.Repositories
{
    public class SeverityRepository : Repository<BugReportSeverity>, ISeverityRepository
    {
        public SeverityRepository(DbContext context) : base(context) { }

        public ApplicationDbContext ApplicationDbContext =>
            context as ApplicationDbContext;

        public void Add(BugReportSeverity item) => ApplicationDbContext.Add(item);

        public async Task AddAsync(BugReportSeverity item)
            => await ApplicationDbContext.AddAsync(item);

        public void Delete(BugReportSeverity item) => ApplicationDbContext.Remove(item);

        public bool Exists(int id)
            => ApplicationDbContext.BugSeverities.Any(severity => severity.Id == id);

        public async Task<bool> ExistsAsync(int id)
            => await ApplicationDbContext.BugSeverities
                .AnyAsync(severity => severity.Id == id);


        public IEnumerable<BugReportSeverity> GetAll()
            => ApplicationDbContext.BugSeverities.AsNoTracking().ToList();

        public async Task<IEnumerable<BugReportSeverity>> GetAllAsync()
            => await ApplicationDbContext.BugSeverities.AsNoTracking().ToListAsync();




        public BugReportSeverity GetById(int id)
            => ApplicationDbContext.BugSeverities.AsNoTracking()
                                 .SingleOrDefault(severity => severity.Id == id);

        public async Task<BugReportSeverity> GetByIdAsync(int id)
            => await ApplicationDbContext.BugSeverities.AsNoTracking()
                                 .SingleOrDefaultAsync(severity => severity.Id == id);



        public async Task<IEnumerable<BugReportSeverity>> GetByQueryAsync(Expression<Func<BugReportSeverity, bool>> query)
            => await ApplicationDbContext.BugSeverities.Where(query)
                                         .AsNoTracking()
                                         .ToListAsync();

        public BugReportSeverity GetEditableById(int id)
        => ApplicationDbContext.BugSeverities.AsTracking()
                        .SingleOrDefault(severity => severity.Id == id);

        public async Task<BugReportSeverity> getEditableByIdAsync(int id)
        => await ApplicationDbContext.BugSeverities.AsTracking()
                        .SingleOrDefaultAsync(severity => severity.Id == id);

        public void SaveChanges() => ApplicationDbContext.SaveChanges();

        public async Task SaveChangesAsync()
            => await ApplicationDbContext.SaveChangesAsync();

        public void Update(BugReportSeverity item) => ApplicationDbContext.Update(item);
    }
}