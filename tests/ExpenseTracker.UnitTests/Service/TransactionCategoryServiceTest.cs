using System;
using System.Threading;
using System.Threading.Tasks;
using ExpenseTracker.Core.Dto.TransactionCategory;
using ExpenseTracker.Core.Entities;
using ExpenseTracker.Core.Entities.Common;
using ExpenseTracker.Core.Exceptions;
using ExpenseTracker.Core.Repositories.Interface;
using ExpenseTracker.Core.Services;
using FluentAssertions;
using Moq;
using Xunit;

namespace ExpenseTracker.UnitTests.Service
{
    public class TransactionCategoryServiceTest
    {
        private readonly Mock<ITransactionCategoryRepository> _transactionCategoryRepository = new();

        private readonly TransactionCategoryService _transactionCategoryService;

        public TransactionCategoryServiceTest()
        {
            _transactionCategoryService = new TransactionCategoryService(_transactionCategoryRepository.Object);
        }

        [Fact]
        public void Test_Create_Method_Create_TransactionCategory()
        {
            var transactionCategoryCreateDto = new TransactionCategoryCreateDto()
            {
                Name = "Office Expense",
                Color = "#ffffff",
                Icon = "office",
                Type = TransactionType.Expense
            };

            _transactionCategoryService.Create(transactionCategoryCreateDto);

            _transactionCategoryRepository.Verify(a => a.InsertAsync(It.Is<TransactionCategory>(x =>
                x.Type == TransactionType.Expense &&
                x.CategoryName == "Office Expense" &&
                x.Icon == "office" &&
                x.Color == "#ffffff"
            ),CancellationToken.None), Times.Once);
        }

        [Fact]
        public void Test_UpdateMethodShouldThrowTransactionCategoryNotFoundException_IfNotFound()
        {
            var transactionCategoryUpdateDto = new TransactionCategoryUpdateDto()
            {
                TransactionCategoryId = 1,
                Name = "Health",
                Color = "#fff000",
                Icon = "ambulance",
            };

            _transactionCategoryRepository
                .Setup(a => a.FindAsync(transactionCategoryUpdateDto.TransactionCategoryId))
                .ReturnsAsync((TransactionCategory)null!);

            Func<Task> updatingTransactionCategory = async () =>
                await _transactionCategoryService.Update(transactionCategoryUpdateDto);

            updatingTransactionCategory.Should().ThrowAsync<TransactionCategoryNotFoundException>()
                .WithMessage(
                    $"Transaction Category with id : {transactionCategoryUpdateDto.TransactionCategoryId} not found.");
        }

        [Fact]
        public async Task Test__UpdatingTransactionCategoryWillUpdateWithCorrectValues()
        {
            var transactionCategory = TransactionCategory.Create(TransactionType.Expense, "hello", "#ffffff", "bla");

            var transactionCategoryUpdateDto = new TransactionCategoryUpdateDto()
            {
                TransactionCategoryId = 1,
                Name = "Health",
                Color = "#fff000",
                Icon = "ambulance",
            };

            _transactionCategoryRepository
                .Setup(a => a.FindAsync(transactionCategoryUpdateDto.TransactionCategoryId))
                .ReturnsAsync(transactionCategory);

            await _transactionCategoryService.Update(transactionCategoryUpdateDto);

            _transactionCategoryRepository.Verify(a => a.UpdateAsync(It.Is<TransactionCategory>(x =>
                x.CategoryName == "Health" &&
                x.Icon == "ambulance" &&
                x.Color == "#fff000"
            ),CancellationToken.None), Times.Once);
        }

        [Fact]
        public void Test_DeleteMethodShouldThrowTransactionCategoryNotFoundException_IfNotFound()
        {
            var transactionCategoryId = 1;

            _transactionCategoryRepository.Setup(a => a.FindAsync(transactionCategoryId))
                .ReturnsAsync((TransactionCategory)null!);

            Func<Task> updatingTransactionCategory = async () =>
                await _transactionCategoryService.Delete(transactionCategoryId);

            updatingTransactionCategory.Should().ThrowAsync<TransactionCategoryNotFoundException>()
                .WithMessage($"Transaction Category with id : {transactionCategoryId} not found.");
        }


        [Fact]
        public async Task Test_DeleteMethodShouldDeleteTransactionCategoryIfFound()
        {
            var transactionCategoryId = 1;

            var transactionCategory = TransactionCategory.Create(TransactionType.Expense, "hello", "#ffffff", "bla");
            typeof(TransactionCategory).GetProperty(nameof(TransactionCategory.Id))
                ?.SetValue(transactionCategory, transactionCategoryId);

            _transactionCategoryRepository.Setup(a => a.FindAsync(transactionCategoryId))
                .ReturnsAsync(transactionCategory);

            await _transactionCategoryService.Delete(transactionCategoryId);

            _transactionCategoryRepository.Verify(a => a.DeleteAsync(It.IsAny<TransactionCategory>(),CancellationToken.None), Times.Once);
        }
    }
}