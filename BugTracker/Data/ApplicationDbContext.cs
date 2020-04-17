using BugTracker.Models;
using BugTracker.Models.Bugs;
using BugTracker.Models.Bugs.Severity;
using BugTracker.Models.Bugs.Status;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

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



        public DbSet<Bug> Bugs { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<BugSeverity> BugSeverities { get; set; }
        public DbSet<BugStatus> BugStatuses { get; set; }
    }
}
