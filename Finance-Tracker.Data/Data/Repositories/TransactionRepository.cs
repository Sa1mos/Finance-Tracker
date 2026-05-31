using FinanceTracker.Data.Interfaces;
using Npgsql;

namespace FinanceTracker.Data.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly NpgsqlDataSource DataSource = null!;

        public TransactionRepository(NpgsqlDataSource dataSource)
        {
            DataSource = dataSource;
        }

        public async Task<Models.Transaction> GetByIdAsync(int id)
        {
            await using var command = DataSource.CreateCommand("SELECT * FROM transactions WHERE id = @id");
            command.Parameters.AddWithValue("@id", id);
            var reader = await command.ExecuteReaderAsync();
            return await reader.ReadAsync() ? new Models.Transaction
            {
                Id = reader.GetInt32(0),
                TransactionCategoryId = reader.GetInt32(1),
                WalletId = reader.GetInt32(2),
                Amount = reader.GetDecimal(3),
                CreatedAt = reader.GetDateTime(4),
                UpdatedAt = reader.GetDateTime(5)
            } : throw new Exception("Transaction not found");
        }

        public async Task<Models.Transaction> GetByNameAsync(string name)
        {
            await using var command = DataSource.CreateCommand("SELECT * FROM transactions WHERE name = @name");
            command.Parameters.AddWithValue("@name", name);
            var reader = await command.ExecuteReaderAsync();
            return await reader.ReadAsync() ? new Models.Transaction
            {
                Id = reader.GetInt32(0),
                TransactionCategoryId = reader.GetInt32(1),
                WalletId = reader.GetInt32(2),
                Amount = reader.GetDecimal(3),
                CreatedAt = reader.GetDateTime(4),
                UpdatedAt = reader.GetDateTime(5)
            } : throw new Exception("Transaction not found");
        }

        public async Task<IEnumerable<Models.Transaction>> GetByWalletIdAsync(int walletId)
        {
            await using var command = DataSource.CreateCommand("SELECT * FROM transactions WHERE wallet_id = @walletId");
            command.Parameters.AddWithValue("@walletId", walletId);
            var reader = await command.ExecuteReaderAsync();
            var transactions = new List<Models.Transaction>();
            while (await reader.ReadAsync())
            {
                transactions.Add(new Models.Transaction
                {
                    Id = reader.GetInt32(0),
                    TransactionCategoryId = reader.GetInt32(1),
                    WalletId = reader.GetInt32(2),
                    Amount = reader.GetDecimal(3),
                    CreatedAt = reader.GetDateTime(4),
                    UpdatedAt = reader.GetDateTime(5)
                });
            }
            return transactions;
        }

        public async Task<IEnumerable<Models.Transaction>> GetAllAsync()
        {
            await using var command = DataSource.CreateCommand("SELECT * FROM transactions");
            var reader = await command.ExecuteReaderAsync();
            var transactions = new List<Models.Transaction>();
            while (await reader.ReadAsync())
            {
                transactions.Add(new Models.Transaction
                {
                    Id = reader.GetInt32(0),
                    TransactionCategoryId = reader.GetInt32(1),
                    WalletId = reader.GetInt32(2),
                    Amount = reader.GetDecimal(3),
                    CreatedAt = reader.GetDateTime(4),
                    UpdatedAt = reader.GetDateTime(5)
                });
            }
            return transactions;
        }

        public async Task<Models.Transaction> AddAsync(Models.Transaction entity)
        {
            await using var command = DataSource.CreateCommand("INSERT INTO transactions (transaction_category_id, wallet_id, amount) VALUES (@transactionCategoryId, @walletId, @amount) RETURNING id");
            command.Parameters.AddWithValue("@transactionCategoryId", entity.TransactionCategoryId);
            command.Parameters.AddWithValue("@walletId", entity.WalletId);
            command.Parameters.AddWithValue("@amount", entity.Amount);
            var reader = await command.ExecuteReaderAsync();
            return await reader.ReadAsync() ? new Models.Transaction
            {
                Id = reader.GetInt32(0),
                TransactionCategoryId = entity.TransactionCategoryId,
                WalletId = entity.WalletId,
                Amount = entity.Amount,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            } : throw new Exception("Failed to add transaction");
        }

        public async Task<Models.Transaction> UpdateAsync(Models.Transaction entity)
        {
            await using var command = DataSource.CreateCommand("UPDATE transactions SET transaction_category_id = @transactionCategoryId, wallet_id = @walletId, amount = @amount WHERE id = @id RETURNING id, transaction_category_id, wallet_id, amount");
            command.Parameters.AddWithValue("@id", entity.Id);
            command.Parameters.AddWithValue("@transactionCategoryId", entity.TransactionCategoryId);
            command.Parameters.AddWithValue("@walletId", entity.WalletId);
            command.Parameters.AddWithValue("@amount", entity.Amount);
            var reader = await command.ExecuteReaderAsync();
            return await reader.ReadAsync() ? new Models.Transaction
            {
                Id = reader.GetInt32(0),
                TransactionCategoryId = reader.GetInt32(1),
                WalletId = reader.GetInt32(2),
                Amount = reader.GetDecimal(3),
                CreatedAt = reader.GetDateTime(4),
                UpdatedAt = reader.GetDateTime(5)
            } : throw new Exception("Failed to update transaction");
        }

        public async Task DeleteAsync(int id)
        {
            await using var command = DataSource.CreateCommand("DELETE FROM transactions WHERE id = @id");
            command.Parameters.AddWithValue("@id", id);
            var rowsAffected = await command.ExecuteNonQueryAsync();
            if (rowsAffected == 0)
            {
                throw new Exception("Currency not found");
            }
        }
    }
}