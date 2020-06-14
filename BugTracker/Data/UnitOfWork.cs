using BugTracker.Data.Repositories;

namespace BugTracker.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext context;

        public IProjectRepository Projects { get; private set; }
        public IUserRepository Users { get; private set; }
        public IBugReportRepository BugReports { get; private set; }
        public ISeverityRepository Severities { get; private set; }
        public IStatusRepository Status { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            this.context = context;
            Projects = new ProjectRepository(context);
            Users = new UserRepository(context);
            BugReports = new BugReportRepository(context);
            Severities = new SeverityRepository(context);
            Status = new StatusRepository(context);

        }
        public int Complete() => context.SaveChanges();

        public void Dispose() => context.Dispose();
    }
}
