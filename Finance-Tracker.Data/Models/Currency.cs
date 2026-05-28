using FinanceTracker.Models.Interfaces;

namespace FinanceTracker.Models
{
    public class Currency : IEntity
    {
        public int Id { get; set; }
        public string CurrencyCode { get; set; } = null!;
        public string CurrencyName { get; set; } = null!;
        public string Symbol { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}