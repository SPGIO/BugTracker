using BugTracker.Data;
using BugTracker.Models.Bugs;
using BugTracker.Models.Bugs.Priority;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BugTracker.Services.Bugs
{
    public interface IBugService
    {
        ApplicationDbContext Context { get; set; }

        Task<int> Add(Bug item);
        Task Delete(Bug item);
        Task<IEnumerable<Bug>> GetAll();
        Task<Bug> GetById(int id);
        Task<IEnumerable<Bug>> GetByPriority(IBugPriority priorty);
        Task<IEnumerable<Bug>> GetByStatus(IBugStatus status);
        Task Update(Bug item);
    }
}