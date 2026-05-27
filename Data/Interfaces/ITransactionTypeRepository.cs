using FinanceTracker.Models;

namespace FinanceTracker.Data.Interfaces
{
    public interface ITransactionTypeRepository : IRepository<Models.TransactionType>
    {
        Task<TransactionType> GetByTypeAsync(string type);

    }
}
