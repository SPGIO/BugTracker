using BugTracker.Models.DomainModels;

namespace BugTracker.Data.Repositories
{
    public interface ISeverityRepository : IRepository<BugReportSeverity>
    {
        bool Exists(int id);
    }
}