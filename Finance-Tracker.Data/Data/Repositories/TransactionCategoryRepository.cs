using FinanceTracker.Data.Interfaces;
using Npgsql;

namespace FinanceTracker.Data.Repositories
{
    public class TransactionCategoryRepository : ITransactionCategoryRepository
    {
        private readonly NpgsqlDataSource DataSource = null!;

        public TransactionCategoryRepository(NpgsqlDataSource dataSource)
        {
            DataSource = dataSource;
        }

        public async Task<Models.TransactionCategory> GetByIdAsync (int Id) 
        { 
            await using var command = DataSource.CreateCommand("SELECT * FROM transaction_categories WHERE id = @id");
            command.Parameters.AddWithValue("@id", Id);
            await using var reader = await command.ExecuteReaderAsync();
            return await reader.ReadAsync() ? new Models.TransactionCategory
            {
                Id = reader.GetInt32(0),
                Category = reader.GetString(1),
                CreatedAt = reader.GetDateTime(2),
                UpdatedAt = reader.GetDateTime(3)
            } : throw new Exception("Transaction category not found");
        }

        public async Task<Models.TransactionCategory> GetByNameAsync(string name)
        {
            await using var command = DataSource.CreateCommand("SELECT * FROM transaction_categories WHERE category = @category");
            command.Parameters.AddWithValue("@category", name);
            await using var reader = await command.ExecuteReaderAsync();
            return await reader.ReadAsync() ? new Models.TransactionCategory
            {
                Id = reader.GetInt32(0),
                Category = reader.GetString(1),
                CreatedAt = reader.GetDateTime(2),
                UpdatedAt = reader.GetDateTime(3)
            } : throw new Exception("Transaction category not found");
        }

        public async Task<IEnumerable<Models.TransactionCategory>> GetAllAsync() 
        {
            await using var command = DataSource.CreateCommand("SELECT * FROM transaction_categories");
            await using var reader = await command.ExecuteReaderAsync();
            var list = new List<Models.TransactionCategory>();
            while (await reader.ReadAsync())
            {
                list.Add(new Models.TransactionCategory
                {
                    Id = reader.GetInt32(0),
                    Category = reader.GetString(1),
                    CreatedAt = reader.GetDateTime(2),
                    UpdatedAt = reader.GetDateTime(3)
                });
            }
            return list;
        }

        public async Task<Models.TransactionCategory> AddAsync(Models.TransactionCategory newTransactionCategory)
        {
            await using var command = DataSource.CreateCommand("INSERT INTO transaction_categories (category) VALUES (@category) RETURNING *");
            command.Parameters.AddWithValue("@category", newTransactionCategory.Category);
            await using var reader = await command.ExecuteReaderAsync();
            return await reader.ReadAsync() ? new Models.TransactionCategory
            {
                Id = reader.GetInt32(0),
                Category = reader.GetString(1),
                CreatedAt = reader.GetDateTime(2),
                UpdatedAt = reader.GetDateTime(3)
            } : throw new Exception("Failed to add transaction category");
        }

        public async Task<Models.TransactionCategory> UpdateAsync(Models.TransactionCategory newTransactionCategory) 
        {
            await using var command = DataSource.CreateCommand("UPDATE transaction_categories SET category = @category WHERE id = @id RETURNING *");
            command.Parameters.AddWithValue("@id", newTransactionCategory.Id);
            command.Parameters.AddWithValue("@category", newTransactionCategory.Category);
            await using var reader = await command.ExecuteReaderAsync();
            return await reader.ReadAsync() ? new Models.TransactionCategory
            {
                Id = reader.GetInt32(0),
                Category = reader.GetString(1),
                CreatedAt = reader.GetDateTime(2),
                UpdatedAt = reader.GetDateTime(3)
            } : throw new Exception("Failed to update transaction category");
        }

        public async Task DeleteAsync(int Id) 
        {
            await using var command = DataSource.CreateCommand("DELETE FROM transaction_categories WHERE id = @id");
            command.Parameters.AddWithValue("@id", Id);
            var rowsAffected = await command.ExecuteNonQueryAsync();
            if (rowsAffected == 0)
            {
                throw new Exception("Transaction category not found");
            }
        }
    }
}
