using FinanceTracker.Models.Interfaces;

namespace FinanceTracker.Models
{
    public class TransactionCategory : IEntity
    {
        public int Id { get; set; }
        public string Category { get; set; } = null!;
        public int TransactionTypeId { get; set; }
    }
}