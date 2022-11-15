using System;
using System.Threading.Tasks;
using System.Transactions;
using ExpenseTracker.Core.Entities;
using ExpenseTracker.Core.Repositories.Interface;
using ExpenseTracker.Core.Services.Interface;

namespace ExpenseTracker.Core.Services
{
    public class BudgetService : IBudgetService
    {
        private readonly IBudgetRepository _budgetRepository;

        public BudgetService(IBudgetRepository budgetRepository)
        {
            _budgetRepository = budgetRepository;
        }

        public async Task Create(BudgetCreateDto dto)
        {
            using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

            var budget = new Budget
            {
                Amount = dto.Amount,
                FromDate = dto.FromDate,
                ToDate = dto.ToDate,
                Workspace = dto.Workspace,
                RecBy = dto.RecBy,
                Description = dto.Description
            };
            await _budgetRepository.InsertAsync(budget);
            await _budgetRepository.CommitAsync();
            scope.Complete();
        }

        public async Task Delete(long budgetId)
        {
            using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

            var budget = await _budgetRepository.FindAsync(budgetId) ??
                         throw new Exception("Budget information not found.");

            await _budgetRepository.DeleteAsync(budget);
            await _budgetRepository.CommitAsync();
            scope.Complete();
        }
    }
}