using FinanceTracker.Models.Interfaces;

namespace FinanceTracker.Models
{
    public class Transaction : IEntity
    {
        public int Id { get; set; }
        public int TransactionCategoryId { get; set; }
        public int WalletId { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
