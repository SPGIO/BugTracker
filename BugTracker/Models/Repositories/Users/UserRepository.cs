using BugTracker.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTracker.Models.Repositories.Users
{
    public class UserRepository : IUserRepository
    {
        public UserRepository(ApplicationDbContext context) => Context = context;

        public ApplicationDbContext Context { get; set; }
        public async Task<IEnumerable<IdentityUser>> GetAll()
            => await Context.Users.ToListAsync();

        public async Task<IdentityUser> GetById(string id)
            => (await GetAll()).FirstOrDefault(user => user.Id == id);

        public async Task<IdentityUser> GetByUsername(string username)
            => (await GetAll()).FirstOrDefault(user => user.UserName == username);
    }
}
