using ExpenseTracker.Core.Entities;
using ExpenseTracker.Core.Entities.Common;
using System;
using Xunit;

namespace ExpenseTracker.UnitTests.Entity
{
    public class TransactionEntityTest
    {
        [Fact]
        public void test_protected_property_sets_correct_value()
        {
            var user = User.Create("admin", "admin");
            var workspace = Workspace.Create(user, "apt", "red");
            var category = TransactionCategory.Create(TransactionType.Income, "cat", "red", "ico");

            var transaction = Transaction.Create(workspace, category, 10, System.DateTime.Now, TransactionType.Income);

            typeof(Transaction).GetProperty(nameof(Transaction.TransactionCategoryId))?
                .SetValue(transaction, 1);
            typeof(Transaction).GetProperty(nameof(Transaction.Amount))?
                .SetValue(transaction, 19.0M);
            typeof(Transaction).GetProperty(nameof(Transaction.WorkspaceId))?
                .SetValue(transaction, 1);
            Assert.Equal(19.0M, transaction.Amount);
            Assert.Equal(1, transaction.WorkspaceId);
            Assert.Equal(1, transaction.TransactionCategoryId);

        }
        [Fact]
        public void test_Update_transasction_date_updates_correct_date()
        {
            var user = User.Create("admin", "admin");
            var workspace = Workspace.Create(user, "apt", "red");
            var category = TransactionCategory.Create(TransactionType.Income, "cat", "red", "ico");
            var transaction = Transaction.Create(workspace, category, 10, System.DateTime.Now, TransactionType.Income);

            transaction.UpdateTransactionDate(DateTime.Now);
            Assert.Equal(DateTime.Now.Date, transaction.TransactionDate.Date);

        }
        [Fact]
        public void test_Update_transasction_amount_updates_correct_amount()
        {
            var user = User.Create("admin", "admin");
            var workspace = Workspace.Create(user, "apt", "red");
            var category = TransactionCategory.Create(TransactionType.Income, "cat", "red", "ico");
            var transaction = Transaction.Create(workspace, category, 10, System.DateTime.Now, TransactionType.Income);

            transaction.UpdateAmount(111);
            Assert.Equal(111M, transaction.Amount);

        }
    }
}
