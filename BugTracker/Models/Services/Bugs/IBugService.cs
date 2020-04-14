using BugTracker.Models.Bugs;
using BugTracker.Models.Bugs.Severity;
using BugTracker.Models.Bugs.Status;
using BugTracker.Models.Repositories;
using BugTracker.Models.Repositories.Bugs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BugTracker.Models.Services.Bugs
{
    public interface IBugService
    {
        IBugRepository Repository { get; }
        Task<int> AddBug(Bug bug);
        Task<bool> DeleteBug(Bug bug);
        Task<IEnumerable<Bug>> GetAllBugs();
        Task<Bug> GetBugById(int? id);
        Task<IEnumerable<Bug>> GetBugsByPriority(IBugSeverity priorty);
        Task<IEnumerable<Bug>> GetBugsByStatus(IBugStatus status);
        Task<bool> UpdateBug(Bug bug);
        Task<bool> BugExists(int id);
    }
}