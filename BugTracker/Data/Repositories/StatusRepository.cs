using BugTracker.Models.DomainModels;
using Microsoft.EntityFrameworkCore;

namespace BugTracker.Data.Repositories
{
    public class StatusRepository : Repository<BugReportStatus>, IStatusRepository
    {
        public StatusRepository(DbContext context) : base(context) { }

    }
}
