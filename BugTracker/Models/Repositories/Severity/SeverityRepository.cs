using BugTracker.Data;
using BugTracker.Models.Bugs.Severity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BugTracker.Models.Repositories.Severity
{
    public class SeverityRepository : IRepository<BugSeverity>, ISeverityRepository
    {
        public SeverityRepository(ApplicationDbContext context)
            => Context = context;

        public ApplicationDbContext Context { get; set; }



        public int Add(BugSeverity item)
        {
            if (item == null) throw new ArgumentNullException();
            Context.Add(item);
            return item.Id;
        }

        public bool Delete(BugSeverity item)
        {
            if (item == null) return false;
            Context.Remove(item);
            return true;
        }

        public async Task<IEnumerable<BugSeverity>> GetAll()
            => await Context.BugSeverities.ToListAsync();


        public async Task<BugSeverity> GetById(int id)
        {
            var severityList =  await GetAll();
            var severityFound = severityList.FirstOrDefault(bug => bug.Id == id);
            if (severityFound == null) throw new KeyNotFoundException();
            return severityFound;
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

        public bool Update(BugSeverity item)
        {
            if (item == null) return false;
            Context.Update(item);
            return true;
        }

        public async Task<bool> Exists(int id)
            => await Context.BugSeverities.AnyAsync(severity => severity.Id == id);

        public void LogError(Exception exception)
            => Debug.WriteLine($"Error!\n{exception.Message}");
    }
}
