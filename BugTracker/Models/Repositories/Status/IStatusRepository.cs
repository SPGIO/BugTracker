using BugTracker.Data;
using BugTracker.Models.Bugs.Status;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BugTracker.Models.Repositories.Status
{
    public interface IStatusRepository
    {
        ApplicationDbContext Context { get; set; }

        int Add(BugStatus item);
        bool Delete(BugStatus item);
        Task<bool> Exists(int id);
        Task<IEnumerable<BugStatus>> GetAll();
        Task<BugStatus> GetById(int id);
        void LogError(Exception exception);
        Task<bool> SaveChangesAsync();
        bool Update(BugStatus item);
    }
}