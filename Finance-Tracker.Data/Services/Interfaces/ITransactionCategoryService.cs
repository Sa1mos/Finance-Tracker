using FinanceTracker.Data.DTOs.TransactionCategory;

namespace FinanceTracker.Data.Services.Interfaces
{
    public interface ITransactionCategoryService
    {
        Task<TransactionCategoryResponseDTO> GetTransactionCategoryAsync(string category);
        
        Task<TransactionCategoryResponseDTO> CreateTransactionCategoryAsync(TransactionCategoryCreateDTO request);

        Task<TransactionCategoryResponseDTO> UpdateTransactionCategoryAsync(TransactionCategoryUpdateDTO request);

        Task DeleteTransactionCategoryAsync(TransactionCategoryDeleteDTO request);

        Task<IEnumerable<TransactionCategoryResponseDTO>> GetAllTransactionCategoriesAsync();
    }
}