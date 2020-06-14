using BugTracker.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTracker.Data.Repositories
{
    public interface IStatusRepository : IRepository<BugReportStatus>
    {
    }
}
