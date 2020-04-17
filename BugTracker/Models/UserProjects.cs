using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTracker.Models
{
    public class UserProjects
    {
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }
    }
}
