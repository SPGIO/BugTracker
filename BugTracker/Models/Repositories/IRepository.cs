using BugTracker.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BugTracker.Models.Repositories
{
    public interface IRepository<T>
    {
        ApplicationDbContext Context { get; set; }
        Task<IEnumerable<T>> GetAll();
        int Add(T item);
        bool Update(T item);
        bool Delete(T item);
        Task<bool> SaveChangesAsync();
    }
}
