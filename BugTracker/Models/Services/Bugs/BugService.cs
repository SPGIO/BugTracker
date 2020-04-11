﻿using BugTracker.Models.Bugs;
using BugTracker.Models.Bugs.Severity;
using BugTracker.Models.Bugs.Status;
using BugTracker.Models.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace BugTracker.Models.Services.Bugs
{
    public class BugService : IBugService
    {
        public IRepository<Bug> Repository { get; }

        public BugService(IRepository<Bug> repository)
        {
            Repository = repository;
        }
        public async Task<IEnumerable<Bug>> GetBugsByPriority(IBugSeverity priorty)
        {
            var allBugs = await Repository.GetAll();
            return allBugs.Where(bug => bug.Severity == priorty);
        }

        public async Task<IEnumerable<Bug>> GetBugsByStatus(IBugStatus status)
        {
            var allBugs = await Repository.GetAll();
            return allBugs.Where(bug => bug.Status == status);
        }

        public async Task<IEnumerable<Bug>> GetAllBugs()
        => await Repository.GetAll();

        public async Task<bool> DeleteBug(Bug bug)
        {
            var isDeleted =  Repository.Delete(bug);
            if (isDeleted)
                await Repository.SaveChangesAsync();
            return isDeleted;
        }

        public async Task<bool> UpdateBug(Bug bug)
        {
            var isModified =  Repository.Update(bug);
            if (isModified)
                await Repository.SaveChangesAsync();
            return isModified;
        }

        public async Task<int> AddBug(Bug bug)
        {
            try
            {
                var bugId = Repository.Add(bug);
                await Repository.SaveChangesAsync();
                return bugId;
            }
            catch (ArgumentNullException)
            {
                throw new BugNotAddedException();
            }
        }

        public async Task<Bug> GetBugById(int id)
        {
            var allBugs = await GetAllBugs();
            Bug bugFound = allBugs.FirstOrDefault(bug => bug.Id == id);
            if (bugFound == null)
                throw new BugNotFoundException();
            return bugFound;
        }
    }
}
