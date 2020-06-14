using BugTracker.Models.DomainModels;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
namespace BugTracker.Models.DomainModels
{
    public class ApplicationUser : IdentityUser
    {
        public virtual ICollection<UserProjects> Projects { get; set; }

        public ApplicationUser()
        {
            Projects = new List<UserProjects>();
        }

    }
}
