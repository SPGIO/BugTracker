using BugTracker.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BugTracker.Models.Repositories.Projects
{
    public interface IProjectRepository
    {
        ApplicationDbContext Context { get; set; }

        int Add(Project item);
        bool Delete(Project item);
        Task<bool> Exists(int id);
        Task<IEnumerable<Project>> GetAll();
        Task<Project> GetById(int? id);
        void LogError(Exception exception);
        Task<bool> SaveChangesAsync();
        bool Update(Project item);
    }
}