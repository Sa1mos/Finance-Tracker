namespace FinanceTracker.Data.DTOs.Wallet
{
    public class WalletCreateDTO
    {
        public string WalletName { get; set; } = null!;
        public string CurrencyName { get; set; } = null!;
    }
}