using System;
using System.Threading.Tasks;
using ExpenseTracker.Core.Dto.TransactionCategory;
using ExpenseTracker.Core.Exceptions;
using ExpenseTracker.Core.Repositories.Interface;
using ExpenseTracker.Core.Services.Interface;
using ExpenseTracker.Infrastructure.Extensions;
using ExpenseTracker.Web.ViewModels.TransactionCategory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ExpenseTracker.Web.Controllers
{
    [Authorize]
    public class TransactionCategoryController : Controller
    {
        private readonly ITransactionCategoryRepository _transactionCategoryRepository;
        private readonly ITransactionCategoryService _transactionCategoryService;
        private readonly ILogger<TransactionCategoryController> _logger;

        public TransactionCategoryController(ITransactionCategoryRepository transactionCategoryRepository,
            ITransactionCategoryService transactionCategoryService,
            ILogger<TransactionCategoryController> logger)
        {
            _transactionCategoryRepository = transactionCategoryRepository;
            _transactionCategoryService = transactionCategoryService;
            _logger = logger;
        }
        
        public async Task<IActionResult> Index(TransactionCategoryIndexViewModel transactionCategoryIndexViewModel)
        {
            var transactionCategories = await _transactionCategoryRepository.GetAllAsync().ConfigureAwait(true);
            transactionCategoryIndexViewModel.TransactionCategories = transactionCategories;
            return View(transactionCategoryIndexViewModel);
        }
        
        [HttpGet]
        public IActionResult Create()
        {
            var transactionCategoryViewModel = new TransactionCategoryViewModel();
            return View(transactionCategoryViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(TransactionCategoryViewModel transactionCategoryViewModel)
        {
            try
            {
                if (!ModelState.IsValid) return View(transactionCategoryViewModel);

                await _transactionCategoryService.Create(new TransactionCategoryCreateDto()
                {
                    Color = transactionCategoryViewModel.Color,
                    Type = transactionCategoryViewModel.Type,
                    Name = transactionCategoryViewModel.Name,
                    Icon = transactionCategoryViewModel.Icon
                });
            
                this.AddSuccessMessage("Transaction Category Create Successfully");
                
            }
            catch (Exception e)
            {
                _logger.LogError(e,e.Message);
                this.AddErrorMessage(e.Message);
            }
            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> Edit(int transactionCategoryId)
        {
            try
            {
                var transactionCategory = await _transactionCategoryRepository.GetByIdAsync(transactionCategoryId)
                    .ConfigureAwait(true) ?? throw new TransactionCategoryNotFoundException();

                var transactionViewModel = new TransactionCategoryViewModel()
                {
                    TransactionCategoryId = transactionCategory.TransactionCategoryId,
                    Name = transactionCategory.CategoryName,
                    Icon = transactionCategory.Icon,
                    Type = transactionCategory.Type,
                    Color = transactionCategory.Color
                };
            
                return View(transactionViewModel);
            }
            catch (Exception e)
            {
                _logger.LogError(e,e.Message);
                this.AddErrorMessage(e.Message);
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(TransactionCategoryViewModel transactionCategoryViewModel)
        {
            try
            {
                if (!ModelState.IsValid) return View(transactionCategoryViewModel);

                await _transactionCategoryService.Update(new TransactionCategoryUpdateDto()
                {
                    TransactionCategoryId = transactionCategoryViewModel.TransactionCategoryId,
                    Color = transactionCategoryViewModel.Color,
                    Type = transactionCategoryViewModel.Type,
                    Name = transactionCategoryViewModel.Name,
                    Icon = transactionCategoryViewModel.Icon
                });
            
                this.AddSuccessMessage("Transaction Category Updated Successfully");
                
            }
            catch (Exception e)
            {
                _logger.LogError(e,e.Message);
                this.AddErrorMessage(e.Message);
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int transactionCategoryId)
        {
            try
            {
                var transactionCategory = await _transactionCategoryRepository.GetByIdAsync(transactionCategoryId)
                    .ConfigureAwait(true) ?? throw new TransactionCategoryNotFoundException();

                await _transactionCategoryService.Delete(transactionCategory.TransactionCategoryId)
                    .ConfigureAwait(true);
                
                this.AddSuccessMessage("Transaction Category Deleted Successfully");
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