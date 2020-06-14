using BugTracker.Models.DomainModels;
using BugTracker.Models.ViewModels.Bugs;
using System.Collections.Generic;

namespace BugTracker.Data.Repositories
{
    public interface IBugReportRepository : IRepository<BugReport>
    {
        IEnumerable<BugReport> GetByProjectId(int id);
        BugReport GetWithSeverityAndStatus(int id);
    }
}