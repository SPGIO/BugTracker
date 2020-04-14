using BugTracker.Data;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BugTracker.Models.Repositories.Users
{
    public interface IUserRepository
    {
        ApplicationDbContext Context { get; set; }

        Task<IEnumerable<IdentityUser>> GetAll();
        Task<IdentityUser> GetById(string id);
        Task<IdentityUser> GetByUsername(string username);
    }
}