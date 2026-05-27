using FinanceTracker.Data.Interfaces;
using Npgsql;
using System.Data;

namespace FinanceTracker.Data.Repositories
{
    public class TransactionCategoryRepository : ITransactionCategory
    {
        private readonly NpgsqlDataSource DataSource = null!;

        public TransactionCategoryRepository(NpgsqlDataSource dataSource)
        {
            DataSource = dataSource;
        }

        public async Task<Models.TransactionCategory> GetByNameAsync(string name) 
        {
            throw new NotImplementedException();
        }

        public async Task<Models.TransactionCategory> GetByIdAsync (int Id) 
        { 
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Models.TransactionCategory>> GetAllAsync() 
        {
            throw new NotImplementedException();
        }

        public async Task<Models.TransactionCategory> AddAsync(Models.TransactionCategory newTransactionCategory) 
        {
            throw new NotImplementedException();
        }

        public async Task<Models.TransactionCategory> UpdateAsync(Models.TransactionCategory newTransactionCategory) 
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(int Id) 
        {
            throw new NotImplementedException();
        }
    }
}
