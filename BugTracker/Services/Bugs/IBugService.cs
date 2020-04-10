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

        Task<int> Add(IBug item);
        Task Delete(IBug item);
        Task<IEnumerable<IBug>> GetAll();
        Task<IBug> GetById(int id);
        Task<IEnumerable<IBug>> GetByPriority(IBugPriority priorty);
        Task<IEnumerable<IBug>> GetByStatus(IBugStatus status);
        Task Update(IBug item);
    }
}