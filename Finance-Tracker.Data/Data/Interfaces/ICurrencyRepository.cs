using FinanceTracker.Models.Interfaces;
using System;

namespace FinanceTracker.Data.Interfaces
{
    public interface ICurrencyRepository : IRepository<Models.Currency>
    {
        Task<Models.Currency> GetByNameAsync(string name);
        Task<Models.Currency> GetByCodeAsync(string code);
    }
}