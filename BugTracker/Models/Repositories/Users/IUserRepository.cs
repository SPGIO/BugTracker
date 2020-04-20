using BugTracker.Data;
using BugTracker.Models.Users;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BugTracker.Models.Repositories.Users
{
    public interface IUserRepository
    {
        ApplicationDbContext Context { get; set; }

        Task<IEnumerable<ApplicationUser>> GetAll();
        Task<ApplicationUser> GetById(string id);
        Task<ApplicationUser> GetByUsername(string username);
    }
}