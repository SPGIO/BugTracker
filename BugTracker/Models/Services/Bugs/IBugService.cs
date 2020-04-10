using BugTracker.Models.Bugs;
using BugTracker.Models.Bugs.Priority;
using BugTracker.Models.Bugs.Status;
using BugTracker.Models.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BugTracker.Models.Services.Bugs
{
    public interface IBugService
    {
        IRepository<Bug> Repository { get; }

        Task<int> AddBug(Bug bug);
        Task<bool> DeleteBug(Bug bug);
        Task<IEnumerable<Bug>> GetAllBugs();
        Task<Bug> GetBugById(int id);
        Task<IEnumerable<Bug>> GetBugsByPriority(IBugPriority priorty);
        Task<IEnumerable<Bug>> GetBugsByStatus(IBugStatus status);
        Task<bool> UpdateBug(Bug bug);
    }
}