using BugTracker.Data;
using BugTracker.Models.Bugs;
using BugTracker.Models.Bugs.Priority;
using BugTracker.Services.Bugs;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTracker.Services
{
    public class BugService : IService<IBug>, IBugService
    {
        public ApplicationDbContext Context { get; set; }
        public BugService(ApplicationDbContext context)
        {
            Context = context;
        }

        public async Task<int> Add(IBug item)
        {
            Context.Add(item);
            await Context.SaveChangesAsync();
            return item.Id;
        }

        public async Task Delete(IBug item)
        {
            Context.Remove(item);
            await Context.SaveChangesAsync();
        }

        public async Task<IEnumerable<IBug>> GetAll()
            => await Context.Bugs
                    .Include(bug => bug.Status)
                    .Include(bug => bug.Priorty)
                    .ToListAsync();


        public Task<IBug> GetById(int id)
            => Context.Bugs.FirstOrDefaultAsync(bug => bug.Id == id);

        public async Task<IEnumerable<IBug>> GetByPriority(IBugPriority priorty)
        {
            var allBugs = await GetAll();
            return allBugs.Where(bug => bug.Priorty == priorty);
        }

        public async Task<IEnumerable<IBug>> GetByStatus(IBugStatus status)
        {
            var allBugs = await GetAll();
            return allBugs.Where(bug => bug.Status == status);
        }

        public async Task Update(IBug item)
        {
            Context.Bugs.Update(item);
            await Context.SaveChangesAsync();
        }
    }
}
