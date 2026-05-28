using FinanceTracker.Models.Interfaces;

namespace FinanceTracker.Models
{
    public class TransactionCategory : IEntity
    {
        public int Id { get; set; }
        public string Category { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}