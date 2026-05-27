using FinanceTracker.Models.Interfaces;

namespace FinanceTracker.Models
{
    public class TransactionType : IEntity
    {
        public int Id { get; set; }
        public string Type { get; set; } = null!;

        public override string ToString()
        {
            return $"Id :{Id}, Type {Type}";
        }

    }
}