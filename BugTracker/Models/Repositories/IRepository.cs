using BugTracker.Data;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BugTracker.Models.Repositories
{
    public interface IRepository<T>
    {
        ApplicationDbContext Context { get; set; }
        Task<IEnumerable<T>> GetAllAsync();
        IEnumerable<T> GetAll();
        void Add(T item);
        Task AddAsync(T item);
        void Update(T item);
        void Delete(T item);
        Task SaveChangesAsync();
        void SaveChanges();
        T GetById(int id);
        Task<T> GetByIdAsync(int id);
        T GetEditableById(int id);
        Task<T> getEditableByIdAsync(int id);

        Task<bool> ExistsAsync(int id);
        bool Exists(int id);

        Task<IEnumerable<T>> GetByQueryAsync(Expression<Func<T, bool>> query);
    }
}
