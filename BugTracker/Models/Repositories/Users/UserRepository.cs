using BugTracker.Data;
using BugTracker.Models.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTracker.Models.Repositories.Users
{
    public class UserRepository : IUserRepository
    {
        public UserRepository(ApplicationDbContext context) => Context = context;

        public ApplicationDbContext Context { get; set; }
        public async Task<IEnumerable<ApplicationUser>> GetAll()
        {
            var allUsers = Context.Users;
            return await allUsers.ToListAsync();
        }

        public async Task<ApplicationUser> GetById(string id)
        {
            var allUsers = await GetAll();
            var userFound = allUsers.SingleOrDefault(user => user.Id == id);
            return userFound;
        }

        public async Task<ApplicationUser> GetByUsername(string username)
            => (await GetAll()).FirstOrDefault(user => user.UserName == username);
    }
}
