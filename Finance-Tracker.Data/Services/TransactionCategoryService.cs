using FinanceTracker.Data.DTOs.TransactionCategory;
using FinanceTracker.Data.Services.Interfaces;
using FinanceTracker.Data.Interfaces;
using Npgsql;

namespace FinanceTracker.Data.Services
{
    public class TransactionCategoryService : ITransactionCategoryService
    {
        private readonly ITransactionCategoryRepository _repository;

        public TransactionCategoryService(ITransactionCategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<TransactionCategoryResponseDTO> CreateTransactionCategoryAsync(TransactionCategoryCreateDTO request)
        {
            try
            {
                var result = await _repository.AddAsync(new Models.TransactionCategory
                {
                Category = request.Category
                });
                return MapToResponseDTO(result);
            }
            catch (NpgsqlException ex) when (ex.SqlState == "23505")
            {
                throw new Exception("Transaction category already exists");
            }
        }

        public async Task DeleteTransactionCategoryAsync(TransactionCategoryDeleteDTO request)
        {
            try
            {
                var categoryId = (await _repository.GetByNameAsync(request.Category)).Id;
                await _repository.DeleteAsync(categoryId);
            }
            catch (NpgsqlException ex) when (ex.SqlState == "23503")
            {
                throw new Exception("Foreign key violation");
            }
        }

        public async Task<IEnumerable<TransactionCategoryResponseDTO>> GetAllTransactionCategoriesAsync()
        {
            var categories = await _repository.GetAllAsync();
            var response = new List<TransactionCategoryResponseDTO>();
            foreach (var category in categories)
            {
                response.Add(MapToResponseDTO(category));
            }
            return response;
        }

        public async Task<TransactionCategoryResponseDTO> GetTransactionCategoryAsync(string category)
        {
            var result = await _repository.GetByNameAsync(category);
            return MapToResponseDTO(result);
        }

        public async Task<TransactionCategoryResponseDTO> UpdateTransactionCategoryAsync(TransactionCategoryUpdateDTO request)
        {
            var existingCategory = await _repository.GetByNameAsync(request.Category);

            try
            {
                var updatedCategory = new Models.TransactionCategory
                {
                    Id = existingCategory.Id,
                    Category = request.NewCategory
                };
                var result = await _repository.UpdateAsync(updatedCategory);
                return MapToResponseDTO(result);
            }
            catch (NpgsqlException ex) when (ex.SqlState == "23505")
            {
                throw new Exception("Transaction category already exists");
            }
        }

        private TransactionCategoryResponseDTO MapToResponseDTO(Models.TransactionCategory model)
        {
            return new TransactionCategoryResponseDTO
            {
                Id = model.Id,
                Category = model.Category,
                CreatedAt = model.CreatedAt,
                UpdatedAt = model.UpdatedAt
            };
        }
    }
}