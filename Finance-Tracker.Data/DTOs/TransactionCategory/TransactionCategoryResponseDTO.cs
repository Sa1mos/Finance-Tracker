namespace FinanceTracker.Data.DTOs.TransactionCategory
{
	public class TransactionCategoryResponseDTO
	{
        public int Id { get; set; }
        public string Category { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
	}
}