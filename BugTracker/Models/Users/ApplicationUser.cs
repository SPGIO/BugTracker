using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
namespace BugTracker.Models.Users
{
    public class ApplicationUser : IdentityUser
    {
        private ICollection<UserProjects> projects;

        public ApplicationUser()
        {
            Projects = new List<UserProjects>();
        }

        public virtual ICollection<UserProjects> Projects
        {
            get { return projects; }
            set
            {
                if (value == null) projects = new List<UserProjects>();
                else projects = value;

            }
        }
    }
}
