using System;
using System.Linq;
using System.Threading.Tasks;
using ExpenseTracker.Core.Dto.Transaction;
using ExpenseTracker.Core.Dto.TransactionCategory;
using ExpenseTracker.Core.Repositories.Interface;
using ExpenseTracker.Core.Services.Interface;
using ExpenseTracker.Infrastructure.Extensions;
using ExpenseTracker.Web.ViewModels;
using ExpenseTracker.Web.ViewModels.Transaction;
using ExpenseTracker.Web.ViewModels.TransactionCategory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ExpenseTracker.Web.Controllers
{
    [Authorize]
    public class TransactionController : Controller
    {
        private readonly ITransactionService _transactionService;
        private readonly ITransactionCategoryRepository _transactionCategoryRepository;
        private readonly ITransactionRepository _transactionRepository;
        private readonly ILogger<TransactionController> _logger;

        public TransactionController(ITransactionService transactionService,
            ITransactionCategoryRepository transactionCategoryRepository,
            ITransactionRepository transactionRepository,
            ILogger<TransactionController> logger)
        {
            _transactionService = transactionService;
            _transactionCategoryRepository = transactionCategoryRepository;
            _transactionRepository = transactionRepository;
            _logger = logger;
        }

        public async Task<IActionResult> Index(TransactionIndexViewModel transactionIndexViewModel)
        {
            var DefaultWorkspaceToken = (await this.GetCurrentUser()).DefaultWorkspace.Token;
            var transactions = _transactionRepository.GetPredicatedQueryable(a=>a.Workspace.Token == DefaultWorkspaceToken).ToList();
            transactionIndexViewModel.Transactions = transactions;
            return View(transactionIndexViewModel);
        }

        public async Task<IActionResult> Create()
        {
            var transactionViewModel = new TransactionViewModel
            {
                TransactionCategories = await _transactionCategoryRepository.GetAllAsync().ConfigureAwait(true)
            };
            return View(transactionViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(TransactionViewModel transactionViewModel)
        {
            try
            {
                if (!ModelState.IsValid) return View(transactionViewModel);

                await _transactionService.Create(new TransactionCreateDto()
                {
                    UserId = this.GetCurrentUserId(),
                    WorkspaceToken = (await this.GetCurrentUser()).DefaultWorkspace.Token, 
                    TransactionDate = transactionViewModel.TransactionEntryDate,
                    Amount = transactionViewModel.TransactionAmount,
                    TransactionCategoryId = transactionViewModel.TransactionCategoryId,
                    Type = transactionViewModel.Type,
                    Description = transactionViewModel.Description
                });
                
                this.AddSuccessMessage("Transaction Created Successfully");
            }
            catch (Exception e)
            {
                _logger.LogError(e,e.Message);
                this.AddErrorMessage(e.Message);
            }
            return RedirectToAction(nameof(Index));
        }
        
    }
}