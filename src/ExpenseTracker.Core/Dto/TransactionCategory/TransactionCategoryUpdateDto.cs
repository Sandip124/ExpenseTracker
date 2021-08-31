namespace ExpenseTracker.Core.Dto.TransactionCategory
{
    public abstract class TransactionCategoryUpdateDto : TransactionCategoryCreateDto
    {
        public int TransactionCategoryId { get; set; }
    }
}