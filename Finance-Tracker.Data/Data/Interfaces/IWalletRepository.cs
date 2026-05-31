using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceTracker.Data.Interfaces
{
    public interface IWalletRepository : IRepository<Models.Wallet>
    {
        Task<Models.Wallet> GetByNameAsync(string name);

        Task<IEnumerable<Models.Wallet>> GetByCurrencyAsync(string currency);

        Task<Models.Projections.WalletWithCurrency> GetWalletWithCurrencyAsync(int id);

        Task<IEnumerable<Models.Projections.WalletWithCurrency>> GetAllWalletsWithCurrencyAsync();
    }
}
