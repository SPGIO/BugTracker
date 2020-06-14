using BugTracker.Models.DomainModels;
using System.ComponentModel.DataAnnotations.Schema;

namespace BugTracker.Models.Users
{
    [NotMapped]
    public class UnknownApplicationUser : ApplicationUser
    {

    }
}
