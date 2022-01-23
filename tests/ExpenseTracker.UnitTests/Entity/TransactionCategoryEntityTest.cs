using ExpenseTracker.Core.Entities;
using ExpenseTracker.Core.Entities.Common;
using ExpenseTracker.Core.Exceptions;
using FluentAssertions;
using System;
using Xunit;

namespace ExpenseTracker.UnitTests.Entity
{
    public class TransactionCategoryEntityTest
    {


        [Fact]
        public void Create_TransactionCategory_CreatesWithCorrectValues()
        {
            var transactionCategory = TransactionCategory.Create(TransactionType.Expense, "Health Expense", "#ffffff", "Medical");

            transactionCategory.CategoryName.Should().Equals("Health Expense");
            transactionCategory.Color.Should().Equals("#ffffff");
            transactionCategory.Icon.Equals("Medical");
            transactionCategory.Transactions.Should().BeEmpty();
        }


        [Fact]
        public void Create_TransactionCategory_WithInvalidTransactionType__ThrowsInvalidTypeException()
        {
            const string? invalidType = "Invalid";

            var creatingTransactionCategory = new Func<TransactionCategory>(() =>
                TransactionCategory.Create(invalidType, "Health Expense", "#ffffff", "Medical"));

            creatingTransactionCategory.Should().Throw<InvalidTransactionTypeException>()
                .WithMessage($"Invalid Transaction Type {invalidType}");
        }
        [Fact]
        public void test_protected_property_categoryId_sets_correct_value()
        {
            var transactionCategory = TransactionCategory.Create(TransactionType.Expense, "Health Expense", "#ffffff", "Medical");

            typeof(TransactionCategory).GetProperty(nameof(TransactionCategory.Id))?
                .SetValue(transactionCategory, 1);
            Assert.Equal(1, transactionCategory.Id);
        }
        [Fact]
        public void test_protected_property_sets_correct_value()
        {
            var transactionCategory = TransactionCategory.Create(TransactionType.Expense, "Health Expense", "#ffffff", "Medical");

            typeof(TransactionCategory).GetProperty(nameof(TransactionCategory.Color))?
                .SetValue(transactionCategory, "colo");
            typeof(TransactionCategory).GetProperty(nameof(TransactionCategory.Icon))?
                .SetValue(transactionCategory, "ico");
            typeof(TransactionCategory).GetProperty(nameof(TransactionCategory.Type))?
                .SetValue(transactionCategory, "Income");
            Assert.Equal("ico", transactionCategory.Icon);
            Assert.Equal("colo", transactionCategory.Color);
            Assert.Equal("Income", transactionCategory.Type);

        }
    }
}