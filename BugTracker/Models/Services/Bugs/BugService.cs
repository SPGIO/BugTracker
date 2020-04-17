using BugTracker.Models.Bugs;
using BugTracker.Models.Bugs.Severity;
using BugTracker.Models.Bugs.Status;
using BugTracker.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BugTracker.Models.Services.Bugs
{
    public class BugService : IBugService
    {
        public IRepository<Bug> Repository { get; }

        public BugService(IRepository<Bug> repository)
        {
            Repository = repository;
        }


        public async Task<IEnumerable<Bug>> GetBugsByPriorityAsync(BugSeverity priorty, bool asTrackable = true)
        {
            if (priorty == null) throw new ArgumentNullException();
            Expression<Func<Bug, bool>> expression = bug => bug.Severity == priorty;
            return await Repository.GetByQueryAsync(expression, asTrackable);
        }

        public IEnumerable<Bug> GetBugsByPriority(BugSeverity priorty, bool asTrackable = true)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Bug>> GetBugsByStatusAsync(BugStatus status, bool asTrackable = true)
        {
            if (status == null) throw new ArgumentNullException();
            Expression<Func<Bug, bool>> expression = bug => bug.Status == status;
            return await Repository.GetByQueryAsync(expression, asTrackable);
        }


        public IEnumerable<Bug> GetBugsByStatus(BugStatus status, bool asTrackable = true)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Bug>> GetAllBugsAsync(bool asTrackable = true)
          => await Repository.GetAllAsync(asTrackable);

        public IEnumerable<Bug> GetAllBugs(bool asTrackable)
          => Repository.GetAll(asTrackable);

        public async Task DeleteBugAsync(Bug bug)
        {
            if (bug == null) throw new BugNotFoundException();
            Repository.Delete(bug);
            await Repository.SaveChangesAsync();
        }

        public void DeleteBug(Bug bug)
        {
            if (bug == null) throw new BugNotFoundException();
            Repository.Delete(bug);
            Repository.SaveChanges();
        }

        public async Task UpdateBugAsync(Bug bug)
        {
            if (bug == null) throw new BugNotFoundException();
            Repository.Update(bug);
            await Repository.SaveChangesAsync();
        }

        public void UpdateBug(Bug bug)
        {
            if (bug == null) throw new BugNotFoundException();
            Repository.Update(bug);
            Repository.SaveChanges();
        }

        public async Task<int> AddBugAsync(Bug bug)
        {
            if (bug == null) throw new BugNotFoundException();
            try
            {
                Repository.Add(bug);
                await Repository.SaveChangesAsync();
                return bug.Id;
            }
            catch (ArgumentNullException)
            {
                throw new BugNotAddedException();
            }
        }

        public int AddBug(Bug bug)
        {
            if (bug == null) throw new BugNotFoundException();
            try
            {
                Repository.Add(bug);
                Repository.SaveChanges();
                return bug.Id;
            }
            catch (ArgumentNullException)
            {
                throw new BugNotAddedException();
            }
        }

        public async Task<Bug> GetBugByIdAsync(int? id, bool asTrackable = true)
        {
            if (id == null) throw new BugNotFoundException();
            var bugFound = await Repository.GetByIdAsync(id.Value, asTrackable);
            if (bugFound == null) throw new BugNotFoundException();
            return bugFound;
        }

        public Bug GetBugById(int? id, bool asTrackable = true)
        {
            if (id == null) throw new BugNotFoundException();
            var bugFound = Repository.GetById(id.Value, asTrackable);
            if (bugFound == null) throw new BugNotFoundException();
            return bugFound;
        }

        public async Task<bool> BugExistsAsync(int id)
            => await Repository.ExistsAsync(id);

        public bool BugExists(int id) => Repository.Exists(id);











    }
}
