namespace ExpenseTracker.Core.Dto.Transaction
{
    public class TransactionUpdateDto : TransactionCreateDto
    {
        public int Id { get; protected set; }
    }
}