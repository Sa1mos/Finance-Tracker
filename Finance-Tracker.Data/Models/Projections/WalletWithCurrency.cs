namespace FinanceTracker.Models.Projections
{
    public class WalletWithCurrency
    {
        public int Id { get; set; }
        public string WalletName { get; set; } = null!;
        public decimal Balance { get; set; }
        public int CurrencyId { get; set; }
        public string CurrencyName { get; set; } = null!;
        public string CurrencySymbol { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
