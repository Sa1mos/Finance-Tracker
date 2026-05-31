using FinanceTracker.Data.Interfaces;
using Npgsql;

namespace FinanceTracker.Data.Repositories
{
    public interface ITransactionRepository : IRepository<Models.Transaction>
    {
        Task<Models.Transaction> GetByNameAsync(string name);

        Task<IEnumerable<Models.Transaction>> GetByWalletIdAsync(int walletId);
    }
}