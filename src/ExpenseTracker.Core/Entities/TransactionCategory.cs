using System.Collections.Generic;
using ExpenseTracker.Core.Entities.Common;
using ExpenseTracker.Core.Exceptions;

namespace ExpenseTracker.Core.Entities
{
    public class TransactionCategory
    {
        public static TransactionCategory Create(string type,string name,string color,string icon)
        {
            if (!TransactionType.IsValidType(type)) throw new InvalidTransactionTypeException(type);
            
            return new TransactionCategory(name,color,icon) {
                Type = type
            };
        }

        protected TransactionCategory() { }
        
        private TransactionCategory(string categoryName,string color,string icon)
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