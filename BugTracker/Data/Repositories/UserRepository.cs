using BugTracker.Models.DomainModels;
using BugTracker.Models.Users;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTracker.Data.Repositories
{
    public class UserRepository : Repository<ApplicationUser>, IUserRepository
    {
        public UserRepository(DbContext context) : base(context) { }

        public ApplicationDbContext ApplicationDbContext =>
            context as ApplicationDbContext;


        public ApplicationUser GetByStringId(string id) =>
            ApplicationDbContext.Users.First(user => user.Id == id);

        public async Task<IEnumerable<ApplicationUser>> GetAll()
        {
            var allUsers = ApplicationDbContext.Users;
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

        public IEnumerable<Project> GetProjectsRelatedToUser(string userId)
        {
            var allProjects =  ApplicationDbContext.Projects;

            return allProjects.Where(project =>
                project.Team.Any(user => user.UserId == userId));

        }
    }
}
