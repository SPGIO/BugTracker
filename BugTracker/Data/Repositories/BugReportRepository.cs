using BugTracker.Models.DomainModels;
using BugTracker.Models.ViewModels.Bugs;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace BugTracker.Data.Repositories
{
    public class BugReportRepository : Repository<BugReport>, IBugReportRepository
    {
        public BugReportRepository(DbContext context) : base(context)
        {
        }

        public BugReport GetWithSeverityAndStatus(int id) =>
            ApplicationDbContext.BugsReports
                .Include(report => report.ReportedBy)
                .Include(report => report.FixedBy)
                .Include(report => report.Severity)
                .Include(report => report.Status)
                .First(report => report.Id == id);


        public IEnumerable<BugReport> GetByProjectId(int id) =>
            ApplicationDbContext.Projects
                .Include(project => project.Bugs)
                .ThenInclude(reports => reports.Severity)
                .Include(project => project.Bugs)
                .ThenInclude(reports => reports.Status)
                .Include(project => project.Bugs)
                .ThenInclude(reports => reports.ReportedBy)
                .Include(project => project.Bugs)
                .ThenInclude(reports => reports.FixedBy)
                .First(project => project.Id == id)
                .Bugs;


        public IEnumerable<BugReportDetailViewModel> GetBugReports()
        {
            var reports = ApplicationDbContext.BugsReports
                .Include(bugReport => bugReport.FixedBy)
                .Include(bugReport => bugReport.ReportedBy)
                .Include(bugReport => bugReport.Severity)
                .Include(bugReport => bugReport.Status)
                .Select(bugReport => new BugReportDetailViewModel
                {
                    Createdby = bugReport.ReportedBy.UserName,
                    DateReported = bugReport.DateReported,
                    HowToReproduce = bugReport.HowToReproduceBug,
                    Severity = bugReport.Severity.Name
                }) ;

            return reports;
        }

        public ApplicationDbContext ApplicationDbContext =>
            context as ApplicationDbContext;
    }

}