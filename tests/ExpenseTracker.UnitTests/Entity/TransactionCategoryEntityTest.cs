using System;
using ExpenseTracker.Core.Entities;
using ExpenseTracker.Core.Entities.Common;
using ExpenseTracker.Core.Exceptions;
using FluentAssertions;
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
    }
}