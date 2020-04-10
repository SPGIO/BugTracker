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
    public class BugService : IService<Bug>, IBugService
    {
        public ApplicationDbContext Context { get; set; }
        public BugService(ApplicationDbContext context)
        {
            Context = context;
        }

        public async Task<int> Add(Bug item)
        {
            Context.Add(item);
            await Context.SaveChangesAsync();
            return item.Id;
        }

        public async Task Delete(Bug item)
        {
            Context.Remove(item);
            await Context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Bug>> GetAll()
            => await Context.Bugs
                    .Include(bug => bug.Status)
                    .Include(bug => bug.Priorty)
                    .ToListAsync();


        public Task<Bug> GetById(int id)
            => Context.Bugs.FirstOrDefaultAsync(bug => bug.Id == id);

        public async Task<IEnumerable<Bug>> GetByPriority(IBugPriority priorty)
        {
            var allBugs = await GetAll();
            return allBugs.Where(bug => bug.Priorty == priorty);
        }

        public async Task<IEnumerable<Bug>> GetByStatus(IBugStatus status)
        {
            var allBugs = await GetAll();
            return allBugs.Where(bug => bug.Status == status);
        }

        public async Task Update(Bug item)
        {
            Context.Bugs.Update(item);
            await Context.SaveChangesAsync();
        }
    }
}
