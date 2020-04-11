using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BugTracker.Data;
using BugTracker.Models.Bugs;
using BugTracker.Models.Bugs.Severity;
using BugTracker.Models.Bugs.Status;
using BugTracker.Models.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BugTracker.Models.Repositories.Bugs
{
    public class BugRepository : IRepository<Bug>
    {
        public ApplicationDbContext Context { get; set; }
        public BugRepository(ApplicationDbContext context)
        {
            Context = context;
        }

        public int Add(Bug item)
        {
            if (item == null) throw new ArgumentNullException();
            Context.Add(item);
            return item.Id;
        }

        public bool Delete(Bug item)
        {
            if (item == null) return false;
            Context.Remove(item);
            return true;
        }

        public async Task<IEnumerable<Bug>> GetAll()
            => await Context.Bugs
                    .Include(bug => bug.Status)
                    .Include(bug => bug.Severity)
                    .ToListAsync();


        public async Task<Bug> GetById(int id)
        {
            var bugFound =  await Context.Bugs
                .FirstOrDefaultAsync(bug => bug.Id == id);
            if (bugFound == null) throw new KeyNotFoundException();
            return bugFound;
        }

        public bool Update(Bug item)
        {
            if (item == null) return false;
            Context.Bugs.Update(item);
            return true;
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


        public void LogError(Exception exception) 
            => Debug.WriteLine($"Error!\n{exception.Message}");
    }
}
