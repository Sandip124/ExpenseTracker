namespace ExpenseTracker.Core.Dto.TransactionCategory
{
    public class TransactionCategoryCreateDto
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Icon { get; set; }
        public string Color { get; set; }
        public int UserId { get; set; }
        public string WorkspaceToken { get; set; }
        
    }
}