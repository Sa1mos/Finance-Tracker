using FinanceTracker.Data.Interfaces;
using Npgsql;

namespace FinanceTracker.Data.Repositories
{
    public class CurrencyRepository : ICurrencyRepository
    {
        private readonly NpgsqlDataSource DataSource = null!;

        public CurrencyRepository(NpgsqlDataSource dataSource)
        {
            DataSource = dataSource;
        }

        public async Task<Models.Currency> GetByIdAsync(int Id) 
        {
            await using var command = DataSource.CreateCommand("SELECT * FROM currencies WHERE id = @id");
            command.Parameters.AddWithValue("@id", Id);
            await using var reader = await command.ExecuteReaderAsync();
            return await reader.ReadAsync() ? new Models.Currency
            {
                Id = reader.GetInt32(0),
                CurrencyCode = reader.GetString(1),
                CurrencyName = reader.GetString(2),
                Symbol = reader.GetString(3),
                CreatedAt = reader.GetDateTime(4),
                UpdatedAt = reader.GetDateTime(5)
            } : throw new Exception("Currency not found");
        }

        public async Task<Models.Currency> GetByNameAsync(string name) 
        {
            await using var command = DataSource.CreateCommand("SELECT * FROM currencies WHERE name = @name");
            command.Parameters.AddWithValue("@name", name);
            await using var reader = await command.ExecuteReaderAsync();
            return await reader.ReadAsync() ? new Models.Currency
            {
                Id = reader.GetInt32(0),
                CurrencyCode = reader.GetString(1),
                CurrencyName = reader.GetString(2),
                Symbol = reader.GetString(3),
                CreatedAt = reader.GetDateTime(4),
                UpdatedAt = reader.GetDateTime(5)
            } : throw new Exception("Currency not found");
        }

        public async Task<Models.Currency> GetByCodeAsync(string code) 
        { 
            await using var command = DataSource.CreateCommand("SELECT * FROM currencies WHERE сode = @code");
            command.Parameters.AddWithValue("@code", code);
            await using var reader = await command.ExecuteReaderAsync();
            return await reader.ReadAsync() ? new Models.Currency
            {
                Id = reader.GetInt32(0),
                CurrencyCode = reader.GetString(1),
                CurrencyName = reader.GetString(2),
                Symbol = reader.GetString(3),
                CreatedAt = reader.GetDateTime(4),
                UpdatedAt = reader.GetDateTime(5)
            } : throw new Exception("Currency not found");
        }

        public async Task<IEnumerable<Models.Currency>> GetAllAsync() 
        {
            var currencies = new List<Models.Currency>();
            await using var command = DataSource.CreateCommand("SELECT * FROM currencies");
            await using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                currencies.Add(new Models.Currency
                {
                    Id = reader.GetInt32(0),
                    CurrencyCode = reader.GetString(1),
                    CurrencyName = reader.GetString(2),
                    Symbol = reader.GetString(3),
                    CreatedAt = reader.GetDateTime(4),
                    UpdatedAt = reader.GetDateTime(5)
                });
            }
            return currencies;
        }

        public async Task<Models.Currency> AddAsync(Models.Currency newCurrency) 
        {
            await using var command = DataSource.CreateCommand("INSERT INTO currencies (code, name, symbol) VALUES (@code, @name, @symbol) RETURNING id, code, name, symbol, created_at, updated_at");
            command.Parameters.AddWithValue("@code", newCurrency.CurrencyCode);
            command.Parameters.AddWithValue("@name", newCurrency.CurrencyName);
            command.Parameters.AddWithValue("@symbol", newCurrency.Symbol);
            await using var reader = await command.ExecuteReaderAsync();
            return await reader.ReadAsync() ? new Models.Currency
            {
                Id = reader.GetInt32(0),
                CurrencyCode = reader.GetString(1),
                CurrencyName = reader.GetString(2),
                Symbol = reader.GetString(3),
                CreatedAt = reader.GetDateTime(4),
                UpdatedAt = reader.GetDateTime(5)
            } : throw new Exception("Failed to insert new currency");
        }

        public async Task<Models.Currency> UpdateAsync(Models.Currency newCurrency) 
        {
            await using var command = DataSource.CreateCommand("UPDATE currencies SET сode = @code, name = @name, symbol = @symbol, WHERE id = @id");
            command.Parameters.AddWithValue("@code", newCurrency.CurrencyCode);
            command.Parameters.AddWithValue("@name", newCurrency.CurrencyName);
            command.Parameters.AddWithValue("@symbol", newCurrency.Symbol);
            command.Parameters.AddWithValue("@id", newCurrency.Id);
            var rowsAffected = await command.ExecuteNonQueryAsync();
            if (rowsAffected == 0)
                throw new Exception("Currency not found");
            return newCurrency;
        }

        public async Task DeleteAsync(int Id) 
        {
            await using var command = DataSource.CreateCommand("DELETE FROM currencies WHERE id = @id");
            command.Parameters.AddWithValue("@id", Id);
            var rowsAffected = await command.ExecuteNonQueryAsync();
            if (rowsAffected == 0)
            {
                throw new Exception("Currency not found");
            }
        }
    }
}