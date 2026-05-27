using FinanceTracker.Models.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinanceTracker.Data.Interfaces
{
    public interface IRepository<T> where T : class, IEntity
    {
        Task<T> GetByIdAsync(int Id);

        Task<IEnumerable<T>> GetAllAsync();

        Task<T> AddAsync(T entity);

        Task<T> UpdateAsync(T entity);

        Task DeleteAsync(int Id);
    }
}