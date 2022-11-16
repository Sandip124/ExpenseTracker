using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ExpenseTracker.Core.Entities.Common;
using ExpenseTracker.Core.Logging;
using ExpenseTracker.Core.Repositories.Interface;
using ExpenseTracker.Infrastructure.Extensions;
using ExpenseTracker.Web.Models;
using ExpenseTracker.Web.Providers.Interface;
using ExpenseTracker.Web.ViewModels.Home;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly ITransactionCategoryRepository _transactionCategoryRepository;
        private readonly IApplicationLogger<HomeController> _logger;
        private readonly IUserProvider _userProvider;

        public HomeController(ITransactionRepository transactionRepository,
            ITransactionCategoryRepository transactionCategoryRepository,
            IApplicationLogger<HomeController> logger, IUserProvider userProvider)
        {
            _transactionRepository = transactionRepository;
            _transactionCategoryRepository = transactionCategoryRepository;
            _logger = logger;
            _userProvider = userProvider;
        }

        public async Task<IActionResult> Index(HomeViewModel homeViewModel)
        {
            try
            {
                var user = await _userProvider.GetCurrentUser();
                var userHasWorkspace = user.HasDefaultWorkspace;
                var workspaceToken = string.Empty;
                if (userHasWorkspace)
                {
                    workspaceToken = user.DefaultWorkspace.Token;
                }

                var transactionQueryable =
                    _transactionRepository.GetPredicatedQueryable(a => a.Workspace.Token == workspaceToken);

                homeViewModel.Transactions = transactionQueryable.Include(a => a.TransactionCategory)
                    .OrderByDescending(a => a.TransactionDate).Take(5).ToList();

                homeViewModel.AllCategories = await _transactionCategoryRepository.GetAllAsync();

                homeViewModel.ExpensesToday = await transactionQueryable
                    .Where(a => a.TransactionDate.Date == DateTime.Now.Date && a.Type == TransactionType.Expense)
                    .SumAsync(a => a.Amount);


                var (firstDay, lastDay) = DateTime.Now.GetDateBound();
                homeViewModel.ExpensesOfTheMonth = await transactionQueryable
                    .Where(a =>  a.TransactionDate.Date >= firstDay.Date && a.TransactionDate.Date <= lastDay.Date && a.Type == TransactionType.Expense)
                    .SumAsync(a => a.Amount);
                
                homeViewModel.IncomeToday = await transactionQueryable
                    .Where(a => a.TransactionDate.Date == DateTime.Now.Date && a.Type == TransactionType.Income)
                    .SumAsync(a => a.Amount);

                homeViewModel.TopExpendingCategories = await transactionQueryable.Include(a => a.TransactionCategory)
                    .OrderByDescending(a => a.Amount)
                    .Take(5).Select(a => new TopCategory()
                        {
                            Amount = a.Amount,
                            CategoryId = a.TransactionCategoryId,
                            CategoryName = a.TransactionCategory.CategoryName,
                            Color = a.TransactionCategory.Color
                        }
                    ).ToListAsync();

                homeViewModel.TransactionSummary = transactionQueryable.Include(a => a.TransactionCategory)
                    .Select(a => new
                    {
                        a.Amount,
                        a.Type,
                        a.TransactionCategory.Color
                    }).ToList()
                    .GroupBy(a => a.Type)
                    .Select(a => new TransactionSummary()
                        {
                            Amount = a.Sum(x => x.Amount),
                            Type = a.Select(x => x.Type).Last(),
                            Color = a.Select(x => x.Color).Last()
                        }
                    ).ToList();


                return View(homeViewModel);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, e);
                return RedirectToAction(nameof(Index));
            }
        }
    }
}