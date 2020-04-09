using BugTracker.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTracker.Services
{
    public interface IService<T>
    {
        ApplicationDbContext Context { get; set; }

        Task<IEnumerable<T>> GetAll();
        Task Add(T item);
        Task Update(T item);
        Task Delete(T item);
    }
}
