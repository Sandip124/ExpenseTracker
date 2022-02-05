namespace ExpenseTracker.Contracts.Dto.Response
{
    public class TransactionCategoryResponseDto
    {
        public long Id { get; set; }
        public virtual string CategoryName { get; set; }
        public virtual string Color { get; set; }
        public virtual string Icon { get; set; }
        public virtual string Type { get; set; }
    }
}