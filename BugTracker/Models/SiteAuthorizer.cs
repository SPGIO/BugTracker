using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BugTracker.Models
{
    public class SiteAuthorizer
    {
        
        public string GetUserId() => User.FindFirstValue(ClaimTypes.NameIdentifier);

        ClaimsPrincipal User { get; }
        public bool IsUserAuthorizedToAccessProject(int projectId)
        {
            return User != null && User.Identity.IsAuthenticated;
        }
    }


}
