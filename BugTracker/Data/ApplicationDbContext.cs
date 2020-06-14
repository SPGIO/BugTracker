using BugTracker.Models.DomainModels;
using BugTracker.Models.Users;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace BugTracker.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserProjects>()
                   .HasKey(userProject =>
                        new { userProject.UserId, userProject.ProjectId });

        }


        public DbSet<UserProjects> UserProjects { get; set; }
        public DbSet<BugReport> BugsReports { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<BugReportSeverity> BugSeverities { get; set; }
        public DbSet<BugReportStatus> BugStatuses { get; set; }
    }
}
