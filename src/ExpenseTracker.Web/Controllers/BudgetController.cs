using System;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreHero.ToastNotification.Abstractions;
using ExpenseTracker.Core.Dto.Transaction;
using ExpenseTracker.Core.Exceptions;
using ExpenseTracker.Core.Logging;
using ExpenseTracker.Core.Repositories.Interface;
using ExpenseTracker.Core.Services.Interface;
using ExpenseTracker.Infrastructure.Extensions;
using ExpenseTracker.Web.Providers.Interface;
using ExpenseTracker.Web.ViewModels;
using ExpenseTracker.Web.ViewModels.Budget;
using ExpenseTracker.Web.ViewModels.Transaction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Web.Controllers
{
    [Authorize]
    public class BudgetController : Controller
    {
        private readonly IApplicationLogger<BudgetController> _logger;
        private readonly IUserProvider _userProvider;
        private readonly INotyfService _notifyService;
        private readonly IBudgetRepository _budgetRepository;
        private readonly IBudgetService _budgetService;

        public BudgetController(
            IApplicationLogger<BudgetController> logger,
            IUserProvider userProvider,
            INotyfService notifyService,
            IBudgetRepository budgetRepository,
            IBudgetService budgetService)
        {
            _logger = logger;
            _userProvider = userProvider;
            _notifyService = notifyService;
            _budgetRepository = budgetRepository;
            _budgetService = budgetService;
        }

        public async Task<IActionResult> Index(BudgetIndexViewModel vm)
        {
            var defaultWorkspaceToken = (await _userProvider.GetDefaultWorkspaceToken());
            var budgets = _budgetRepository
                .GetPredicatedQueryable(a => a.Workspace.Token == defaultWorkspaceToken)
                .OrderByDescending(x => x.Id)
                .ToList();
            vm.Budgets = budgets;
            return View(vm);
        }

        public async Task<IActionResult> SetBudget()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SetBudget(BudgetViewModel vm)
        {
            try
            {
                await _budgetService.Create(new BudgetCreateDto()
                {
                    RecBy = await _userProvider.GetCurrentUser(),
                    Workspace = await _userProvider.GetDefaultWorkspace(),
                    Amount = vm.Amount,
                    FromDate = vm.FromDate,
                    ToDate = vm.ToDate,
                    Description = vm.Description
                });

                _notifyService.Success($"Budget set Successfully for [{vm.FromDate:d} to {vm.ToDate:d}]");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, e);
                _notifyService.Error(e.Message);
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                var budget = await _budgetRepository.FindAsync(id)
                             ?? throw new Exception("Budget not found.");

                await _budgetService.Delete(budget.Id);

                _notifyService.Success("Budget Deleted Successfully");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, e);
                _notifyService.Error(e.Message);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}