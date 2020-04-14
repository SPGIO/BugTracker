using BugTracker.Data;
using BugTracker.Models.Bugs.Severity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BugTracker.Models.Repositories.Severity
{
    public interface ISeverityRepository
    {
        ApplicationDbContext Context { get; set; }

        int Add(BugSeverity item);
        bool Delete(BugSeverity item);
        Task<bool> Exists(int id);
        Task<IEnumerable<BugSeverity>> GetAll();
        Task<BugSeverity> GetById(int id);
        void LogError(Exception exception);
        Task<bool> SaveChangesAsync();
        bool Update(BugSeverity item);
    }
}