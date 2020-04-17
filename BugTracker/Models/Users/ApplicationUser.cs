using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
namespace BugTracker.Models.Users
{
    public class ApplicationUser : IdentityUser
    {
        public virtual IEnumerable<UserProjects> Projects { get; set; }
    }
}
