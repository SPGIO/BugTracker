using BugTracker.Data.Repositories;
using System;

namespace BugTracker.Data
{
    public interface IUnitOfWork : IDisposable
    {
        IProjectRepository Projects { get; }
        IUserRepository Users { get; }
        IBugReportRepository BugReports { get; }
        ISeverityRepository Severities { get; }
        IStatusRepository Status { get; }
        int Complete();
    }
}
