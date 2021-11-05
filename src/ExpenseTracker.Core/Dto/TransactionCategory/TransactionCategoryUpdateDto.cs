namespace ExpenseTracker.Core.Dto.TransactionCategory
{
    public class TransactionCategoryUpdateDto : TransactionCategoryCreateDto
    {
        public long TransactionCategoryId { get; set; }
    }
}