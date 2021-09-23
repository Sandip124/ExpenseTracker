namespace ExpenseTracker.Core.Dto.TransactionCategory
{
    public class TransactionCategoryUpdateDto : TransactionCategoryCreateDto
    {
        public int TransactionCategoryId { get; set; }
    }
}