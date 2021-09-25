using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ExpenseTracker.Core.Entities.Common;
using ExpenseTracker.Core.Repositories.Interface;
using ExpenseTracker.Infrastructure.Extensions;
using ExpenseTracker.Web.Models;
using ExpenseTracker.Web.ViewModels.Home;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ExpenseTracker.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly ITransactionCategoryRepository _transactionCategoryRepository;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ITransactionRepository transactionRepository,ITransactionCategoryRepository transactionCategoryRepository,ILogger<HomeController> logger)
        {
            _transactionRepository = transactionRepository;
            _transactionCategoryRepository = transactionCategoryRepository;
            _logger = logger;
        }

        public async Task<IActionResult> Index(HomeViewModel homeViewModel)
        {
            var workspaceToken = (await this.GetCurrentUser()).DefaultWorkspace.Token;

            var transactionQueryable = _transactionRepository.GetPredicatedQueryable(a => a.Workspace.Token == workspaceToken);
            
            homeViewModel.Transactions = transactionQueryable.OrderByDescending(a=>a.TransactionDate).Take(5).ToList();
                homeViewModel.TopExpendingCategories = transactionQueryable
                    .Where(a => a.Type == TransactionType.Expense)
                    .OrderByDescending(a => a.Amount)
                    .Select(x => new TopCategory()
                    {
                        CategoryName = x.TransactionCategory.CategoryName,
                        Amount = x.Amount,
                        CategoryId = x.TransactionCategoryId
                    }).ToList();
                homeViewModel.AllCategories = await _transactionCategoryRepository.GetAllAsync().ConfigureAwait(true);
                homeViewModel.DailyExpenseAmount = transactionQueryable.Where(a =>
                        a.TransactionDate.Date == DateTime.Today.Date && a.Type == TransactionType.Expense).ToList()
                    .Sum(a => a.Amount);
                homeViewModel.DailyIncomeAmount = transactionQueryable.Where(a =>
                        a.TransactionDate.Date == DateTime.Today.Date && a.Type == TransactionType.Income).ToList()
                    .Sum(a => a.Amount);
                homeViewModel.WeeklyExpenseAmount = transactionQueryable.Where(a =>
                        a.TransactionDate.Date >= DateTime.Today.AddDays(-7).Date &&
                        a.TransactionDate.Date <= DateTime.Today.Date && a.Type == TransactionType.Expense).ToList()
                    .Sum(a => a.Amount);
                homeViewModel.WeeklyIncomeAmount = transactionQueryable.Where(a =>
                        a.TransactionDate.Date >= DateTime.Today.AddDays(-7).Date &&
                        a.TransactionDate.Date <= DateTime.Today.Date && a.Type == TransactionType.Income).ToList()
                    .Sum(a => a.Amount);
                homeViewModel.MonthlyExpenseAmount = transactionQueryable.Where(a =>
                        a.TransactionDate.Date >= DateTime.Today.AddMonths(-1).Date &&
                        a.TransactionDate.Date <= DateTime.Today.Date && a.Type == TransactionType.Expense).ToList()
                    .Sum(a => a.Amount);
                homeViewModel.MonthlyIncomeAmount = transactionQueryable.Where(a =>
                        a.TransactionDate.Date >= DateTime.Today.AddMonths(-1).Date &&
                        a.TransactionDate.Date <= DateTime.Today.Date && a.Type == TransactionType.Income).ToList()
                    .Sum(a => a.Amount);
                    
                return View(homeViewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}