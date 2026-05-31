namespace FinanceTracker.Data.DTOs.Wallet
{
    public class WalletResponseDTO
    {
        public int Id { get; set; }
        public string WalletName { get; set; } = null!;
        public decimal Balance { get; set; }
        public string CurrencyName { get; set; } = null!;
        public string CurrencySymbol { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}