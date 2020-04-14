using BugTracker.Data;
using BugTracker.Models.Bugs.Status;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BugTracker.Models.Repositories.Status
{
    public class StatusRepository : IRepository<BugStatus>, IStatusRepository
    {
        public StatusRepository(ApplicationDbContext context)
        {
            Context = context;
        }

        public ApplicationDbContext Context { get; set; }

        public int Add(BugStatus item)
        {
            if (item == null) throw new ArgumentNullException();
            Context.Add(item);
            return item.Id;
        }

        public bool Delete(BugStatus item)
        {
            if (item == null) return false;
            Context.Remove(item);
            return true;
        }

        public async Task<IEnumerable<BugStatus>> GetAll()
            => await Context.BugStatuses.ToListAsync();

        public async Task<BugStatus> GetById(int id)
        {
            var listOfStatuses = await GetAll();
            var statusFound = listOfStatuses
                .FirstOrDefault(status => status.Id == id);
            if (statusFound == null) throw new KeyNotFoundException();
            return statusFound;
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

        public bool Update(BugStatus item)
        {
            if (item == null) return false;
            Context.Update(item);
            return true;
        }
        public async Task<bool> Exists(int id)
           => await Context.BugStatuses.AnyAsync(status => status.Id == id);

        public void LogError(Exception exception)
            => Debug.WriteLine($"Error!\n{exception.Message}");
    }
}
