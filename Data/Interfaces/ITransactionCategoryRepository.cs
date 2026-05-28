using FinanceTracker.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceTracker.Data.Interfaces
{
    public interface ITransactionCategory : IRepository<Models.TransactionCategory>
    {
        Task<Models.TransactionCategory> GetByNameAsync(string name);
    }
}
