using BugTracker.Data;
using BugTracker.Models.Bugs;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTracker.Services
{
    public class BugService : IService<IBug>, IBugService
    {
        public BugService(ApplicationDbContext context)
        {
            Context = context;
        }

        public ApplicationDbContext Context { get; set; }



        public async Task Add(IBug item)
        {
            Context.Add(item);
            await Context.SaveChangesAsync();
        }

        public async Task Delete(IBug item)
        {
            Context.Remove(item);
            await Context.SaveChangesAsync();
        }

        public async Task<IEnumerable<IBug>> GetAll()
            => await Context.Bugs.ToListAsync();

        public Task<IBug> GetById(int id)
            => Context.Bugs.FirstOrDefaultAsync(bug => bug.Id == id);

        public async Task<IEnumerable<IBug>> GetByPriority(IBugPriorty priorty)
            => await Context.Bugs
                    .Where(bug => bug.Priorty == priorty)
                    .ToListAsync();

        public async Task<IEnumerable<IBug>> GetByStatus(IBugPriorty priorty)
            => await Context.Bugs
                    .Where(bug => bug.Priorty == priorty)
                    .ToListAsync();

        public async Task Update(IBug item)
        {
            Context.Bugs.Update(item);
            await Context.SaveChangesAsync();
        }
    }
}
