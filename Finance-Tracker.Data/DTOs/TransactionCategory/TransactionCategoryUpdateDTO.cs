namespace FinanceTracker.Data.DTOs.TransactionCategory
{
    public class TransactionCategoryUpdateDTO
    {
        public string Category { get; set; } = null!;

        public string NewCategory { get; set; } = null!;
    }
}