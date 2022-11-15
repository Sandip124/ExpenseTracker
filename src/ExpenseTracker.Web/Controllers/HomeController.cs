using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ExpenseTracker.Core.Logging;
using ExpenseTracker.Core.Repositories.Interface;
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
            IApplicationLogger<HomeController> logger,IUserProvider userProvider)
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

                var transactionQueryable = _transactionRepository.GetPredicatedQueryable(a => a.Workspace.Token == workspaceToken).Include(a=>a.TransactionCategory);
            
                homeViewModel.Transactions = transactionQueryable.OrderByDescending(a=>a.TransactionDate).Take(5).ToList();
                
                homeViewModel.AllCategories = await _transactionCategoryRepository.GetAllAsync();

                    
                return View(homeViewModel);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message,e);
                return RedirectToAction(nameof(Index));
            }
        }
    }
}