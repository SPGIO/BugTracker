using BugTracker.Data;
using BugTracker.Models.Bugs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BugTracker.Services.Bugs
{
    public interface IBugService
    {
        ApplicationDbContext Context { get; set; }

        Task Add(IBug item);
        Task Delete(IBug item);
        Task<IEnumerable<IBug>> GetAll();
        Task<IBug> GetById(int id);
        Task<IEnumerable<IBug>> GetByPriority(IBugPriorty priorty);
        Task<IEnumerable<IBug>> GetByStatus(IBugPriorty priorty);
        Task Update(IBug item);
    }
}