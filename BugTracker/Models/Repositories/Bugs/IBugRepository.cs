using BugTracker.Data;
using BugTracker.Models.Bugs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BugTracker.Models.Repositories.Bugs
{
    public interface IBugRepository
    {
        ApplicationDbContext Context { get; set; }

        int Add(Bug item);
        bool Delete(Bug item);
        Task<IEnumerable<Bug>> GetAll();
        Task<Bug> GetById(int id);
        void LogError(Exception exception);
        Task<bool> SaveChangesAsync();
        bool Update(Bug item);

        Task<bool> Exists(int Id);
    }
}