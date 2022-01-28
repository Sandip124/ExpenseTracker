using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreHero.ToastNotification.Abstractions;
using ExpenseTracker.Core.Dto.Transaction;
using ExpenseTracker.Core.Dto.TransactionCategory;
using ExpenseTracker.Core.Entities;
using ExpenseTracker.Core.Exceptions;
using ExpenseTracker.Core.Repositories.Interface;
using ExpenseTracker.Core.Services.Interface;
using ExpenseTracker.Infrastructure.Extensions;
using ExpenseTracker.Web.Providers;
using ExpenseTracker.Web.Providers.Interface;
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
        private readonly IUserProvider _userProvider;
        private readonly INotyfService _notifyService;

        public TransactionController(ITransactionService transactionService,
            ITransactionCategoryRepository transactionCategoryRepository,
            ITransactionRepository transactionRepository,
            ILogger<TransactionController> logger, IUserProvider userProvider, INotyfService notifyService)
        {
            _transactionService = transactionService;
            _transactionCategoryRepository = transactionCategoryRepository;
            _transactionRepository = transactionRepository;
            _logger = logger;
            _userProvider = userProvider;
            _notifyService = notifyService;
        }

        public async Task<IActionResult> Index(TransactionIndexViewModel transactionIndexViewModel)
        {
            var defaultWorkspaceToken = (await _userProvider.GetCurrentUser()).DefaultWorkspace.Token;
            var transactions = _transactionRepository.GetPredicatedQueryable(a => a.Workspace.Token == defaultWorkspaceToken)
                .OrderByDescending(x => x.TransactionDate)
                .ToList();
            transactionIndexViewModel.Transactions = transactions;
            return View(transactionIndexViewModel);
        }

        public async Task<IActionResult> Create()
        {
            var transactionViewModel = new TransactionViewModel
            {
                TransactionCategories = await _transactionCategoryRepository.GetAllAsync()
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
                    UserId = _userProvider.GetCurrentUserId(),
                    WorkspaceToken = (await _userProvider.GetCurrentUser()).DefaultWorkspace.Token,
                    TransactionDate = transactionViewModel.TransactionEntryDate,
                    Amount = transactionViewModel.TransactionAmount,
                    TransactionCategoryId = transactionViewModel.TransactionCategoryId,
                    Type = transactionViewModel.Type,
                    Description = transactionViewModel.Description
                });

                _notifyService.Success("Transaction Created Successfully");
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                this.AddErrorMessage(e.Message);
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var transaction = await _transactionRepository.FindAsync(id)
                                  ?? throw new TransactionNotFoundException();

                var transactionViewModel = new TransactionViewModel()
                {
                    Amount = transaction.Amount
                };

                return View(transactionViewModel);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                this.AddErrorMessage(e.Message);
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(TransactionViewModel transactionViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    await _transactionService.Update(new TransactionUpdateDto()
                    {
                        Amount = transactionViewModel.Amount,
                        Id = transactionViewModel.Id,
                    });
                }

                _notifyService.Success("Transaction  Updated Successfully");
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                this.AddErrorMessage(e.Message);
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var transaction = await _transactionRepository.FindAsync(id)
                                  ?? throw new TransactionNotFoundException();

                await _transactionService.Delete(transaction.Id);

                _notifyService.Success("Transaction  Deleted Successfully");
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                this.AddErrorMessage(e.Message);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}