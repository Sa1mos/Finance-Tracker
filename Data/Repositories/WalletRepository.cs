using FinanceTracker.Data.Interfaces;
using Npgsql;

namespace FinanceTracker.Data.Repositories
{
    public class WalletRepository : IWalletRepository
    {
        private readonly NpgsqlDataSource DataSource = null!;

        public WalletRepository(NpgsqlDataSource dataSource) 
        {
            DataSource = dataSource;
        }

        public async Task<Models.Wallet> GetByIdAsync(int id) 
        {
            await using var command = DataSource.CreateCommand("SELECT * FROM wallet WHERE id = @id");
            command.Parameters.AddWithValue("@id", id);
            await using var reader = await command.ExecuteReaderAsync();
            return await reader.ReadAsync() ? new Models.Wallet
            {
                Id = reader.GetInt32(0),
                WalletName = reader.GetString(1),
                Balance = reader.GetDecimal(2)
            } : throw new Exception("Wallet not found");

        }

        public async Task<Models.Wallet> GetByNameAsync(string name)
        {
            await using var command = DataSource.CreateCommand("SELECT * FROM wallet WHERE wallet = @wallet");
            command.Parameters.AddWithValue("@wallet", name);
            await using var reader  = await command.ExecuteReaderAsync();
            return await reader.ReadAsync() ? new Models.Wallet
            {
                Id = reader.GetInt32(0),
                WalletName = reader.GetString(1),
                Balance = reader.GetDecimal(2)
            } : throw new Exception("Wallet not found");
        }

        public async Task<IEnumerable<Models.Wallet>> GetAllAsync() 
        {
            await using var command = DataSource.CreateCommand("SELECT * FROM wallet");
            await using var reader = await command.ExecuteReaderAsync();
            var list = new List<Models.Wallet>();
            while (await reader.ReadAsync()) 
            {
                list.Add(new Models.Wallet {
                    Id = reader.GetInt32(0),
                    WalletName = reader.GetString(1),
                    Balance= reader.GetDecimal(2)
                });
            }
            return list;
        }

        public async Task<Models.Wallet> AddAsync(Models.Wallet newWallet)
        {
            await using var command = DataSource.CreateCommand("INSERT INTO wallet (wallet) VALUES (@walletName) RETURNING *");
            command.Parameters.AddWithValue("@walletName", newWallet.WalletName);
            using var reader = await command.ExecuteReaderAsync();
            return await reader.ReadAsync() ? new Models.Wallet 
            {
                Id= reader.GetInt32(0),
                WalletName = reader.GetString(1),
                Balance = reader.GetDecimal(2)
            } : throw new Exception("Failed to insert new wallet");
        }

        public async Task<Models.Wallet> UpdateAsync(Models.Wallet wallet) 
        {
            await using var command = DataSource.CreateCommand("UPDATE wallet SET wallet = @walletName WHERE id = @id RETURNING *");
            command.Parameters.AddWithValue("@walletName", wallet.WalletName);
            command.Parameters.AddWithValue("@id", wallet.Id);
            await using var reader = await command.ExecuteReaderAsync();
            return await reader.ReadAsync() ? new Models.Wallet
            {
                Id = reader.GetInt32(0),
                WalletName= reader.GetString(1),
                Balance = reader.GetDecimal(2)
            } : throw new Exception("Updatable field not found");
        }

        public async Task DeleteAsync(int id) 
        {
            await using var command = DataSource.CreateCommand("DELETE FROM wallet WHERE id = @id RETURNING *");
            command.Parameters.AddWithValue("@id", id);
            int rowsAffected = await command.ExecuteNonQueryAsync();
            if(rowsAffected == 0) 
            {
                throw new Exception("Deletable field not found");
            }
            
        }
    }
}
