using System.Collections.Generic;

namespace ExpenseTracker.Core.Entities
{
    public class TransactionCategory
    {
        public static TransactionCategory CreateExpensesCategory(string name,string color,string icon)
        {
            return new(name,color,icon)
            {
                Type = TransactionType.Expense
            };
        }

        public static TransactionCategory CreateIncomeCategory(string name,string color,string icon)
        {
            return new(name,color,icon)
            {
                Type = TransactionType.Income
            };
        }

        protected TransactionCategory() { }
        
        public TransactionCategory(string categoryName,string color,string icon)
        {
            CategoryName = categoryName;
            Color = color;
            Icon = icon;
        }

        public virtual int TransactionCategoryId { get; protected set; }

        public virtual string CategoryName { get; protected set; }

        public virtual void UpdateName(string name) => CategoryName = name;

        public virtual string Color { get; protected set; }
        public virtual void UpdateColor(string color) => Color = color;
        public virtual string Icon { get; protected set; }
        public virtual void UpdateIcon(string icon) => Icon = icon;

        public virtual string Type { get; protected set; }

        public virtual ICollection<Transaction> Transactions { get; set; }

        
    }
}