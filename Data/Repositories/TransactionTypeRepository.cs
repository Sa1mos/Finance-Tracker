using FinanceTracker.Data.Interfaces;
using FinanceTracker.Models;
using FinanceTracker.Models.Interfaces;
using Npgsql;

namespace FinanceTracker.Data.Repositories
{
    public class TransactionTypeRepository : ITransactionTypeRepository
    {
        private readonly NpgsqlDataSource DataSource = null!;

        public TransactionTypeRepository(NpgsqlDataSource dataSource)
        {
            DataSource = dataSource;
        }

        public async Task<Models.TransactionType> GetByTypeAsync(string type)
        {
            await using var command = DataSource.CreateCommand("SELECT * FROM transaction_type WHERE transaction_type = @type");
            command.Parameters.AddWithValue("type", type);
            await using var reader = await command.ExecuteReaderAsync();
            return await reader.ReadAsync() ? new Models.TransactionType
            {
                Id = reader.GetInt32(0),
                Type = reader.GetString(1)
            } : throw new Exception("Transaction type not found");
        }

        public async Task<Models.TransactionType> GetByIdAsync(int id) 
        {
            await using var command = DataSource.CreateCommand("SELECT * FROM transaction_type WHERE id = @id");
            command.Parameters.AddWithValue("id", id);
            await using var reader = await command.ExecuteReaderAsync();
            return await reader.ReadAsync() ? new Models.TransactionType
            {
                Id = reader.GetInt32(0),
                Type = reader.GetString(1)
            } : throw new Exception("Transaction type not found");

        }

        public async Task<IEnumerable<Models.TransactionType>> GetAllAsync() 
        {
            await using var command = DataSource.CreateCommand("SELECT * FROM transaction_type");
            await using var reader = await command.ExecuteReaderAsync();
            var list = new List<Models.TransactionType>();
            while (await reader.ReadAsync())
            {
                list.Add(new Models.TransactionType
                {
                    Id = reader.GetInt32(0),
                    Type = reader.GetString(1)
                });
            }
            return list;
        }

        public async Task<Models.TransactionType> AddAsync(Models.TransactionType newEntity)
        {
            await using var command = DataSource.CreateCommand("INSERT INTO transaction_type (transaction_type) VALUES (@type) RETURNING id");
            command.Parameters.AddWithValue("@type", newEntity.Type);
            await using var reader = await command.ExecuteReaderAsync();
            newEntity.Id = await reader.ReadAsync() ? reader.GetInt32(0) : throw new Exception("Failed to insert new transaction type");
            return newEntity;
        }

        public async Task<Models.TransactionType> UpdateAsync(Models.TransactionType entity)
        {
            await using var command = DataSource.CreateCommand("UPDATE transaction_type SET transaction_type = @type WHERE id = @id RETURNING *");
            command.Parameters.AddWithValue("@type", entity.Type);
            command.Parameters.AddWithValue("@id", entity.Id);
            await using var reader = await command.ExecuteReaderAsync();
            return await reader.ReadAsync() ? new Models.TransactionType
            {
                Id = reader.GetInt32(0),
                Type = reader.GetString(1)
            } : throw new Exception("Updatable field not found");
        }

        public async Task DeleteAsync(int Id)
        {
            await using var command = DataSource.CreateCommand("DELETE FROM transaction_type WHERE id = @id RETURNING *");
            command.Parameters.AddWithValue("@id", Id);
            int rowsAffected = await command.ExecuteNonQueryAsync();
            if (rowsAffected == 0) 
            {
                throw new Exception("Deletable field not found");
            }
        }
    }

}