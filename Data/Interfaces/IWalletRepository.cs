using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceTracker.Data.Interfaces
{
    internal interface IWalletRepository : IRepository<Models.Wallet>
    {
        Task<Models.Wallet> GetByNameAsync(string name);
    }
}
