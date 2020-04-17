using Microsoft.AspNetCore.Identity;
using System.Collections;
using System.Collections.Generic;

namespace BugTracker.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser() => Projects = new HashSet<Project>();
        public virtual ICollection<Project> Projects { get; set; }
    }
}
