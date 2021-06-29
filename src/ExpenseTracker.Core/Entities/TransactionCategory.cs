namespace ExpenseTracker.Core.Entities
{
    public class TransactionCategory
    {
        public static TransactionCategory CreateExpensesCategory(string name,string color,string icon)
        {
            return new(name,color,icon)
            {
                Type = TransactionType.Expenses
            };
        }

        public static TransactionCategory CreateIncomeCategory(string name,string color,string icon)
        {
            return new(name,color,icon)
            {
                Type = TransactionType.Income
            };
        }

        
        public TransactionCategory(string name,string color,string icon)
        {
            Name = name;
        }

        public int Id { get; protected set; }

        public string Name { get; protected set; }

        public string Color { get; protected set; }
        public string Icon { get; protected set; }

        public TransactionType Type { get; protected set; }

        
    }
}