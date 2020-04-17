using BugTracker.Models.Bugs;
using BugTracker.Models.Bugs.Severity;
using BugTracker.Models.Bugs.Status;
using BugTracker.Models.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BugTracker.Models.Services.Bugs
{
    public interface IBugService
    {
        IRepository<Bug> Repository { get; }

        int AddBug(Bug bug);
        Task<int> AddBugAsync(Bug bug);
        bool BugExists(int id);
        Task<bool> BugExistsAsync(int id);
        void DeleteBug(Bug bug);
        Task DeleteBugAsync(Bug bug);
        IEnumerable<Bug> GetAllBugs(bool asTrackable = true);
        Task<IEnumerable<Bug>> GetAllBugsAsync(bool asTrackable = true);
        Bug GetBugById(int? id, bool asTrackable = true);
        Task<Bug> GetBugByIdAsync(int? id, bool asTrackable = true);
        IEnumerable<Bug> GetBugsByPriority(BugSeverity priorty, bool asTrackable = true);
        Task<IEnumerable<Bug>> GetBugsByPriorityAsync(BugSeverity priorty, bool asTrackable = true);
        IEnumerable<Bug> GetBugsByStatus(BugStatus status, bool asTrackable = true);
        Task<IEnumerable<Bug>> GetBugsByStatusAsync(BugStatus status, bool asTrackable = true);
        void UpdateBug(Bug bug);
        Task UpdateBugAsync(Bug bug);
    }
}