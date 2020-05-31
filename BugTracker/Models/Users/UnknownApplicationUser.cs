using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BugTracker.Models.Users
{
    [NotMapped]
    public class UnknownApplicationUser : ApplicationUser
    {

    }
}
