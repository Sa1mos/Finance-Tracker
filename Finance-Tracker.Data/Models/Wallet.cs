using FinanceTracker.Models.Interfaces;

namespace FinanceTracker.Models
{
    public class Wallet : IEntity
    {
        public int Id { get; set; }
        public string WalletName { get; set; } = null!;
        public decimal Balance { get; set; }
        public string Currency { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
