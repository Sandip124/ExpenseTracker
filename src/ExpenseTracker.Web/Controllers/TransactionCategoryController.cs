using System;
using System.Threading.Tasks;
using AspNetCoreHero.ToastNotification.Abstractions;
using ExpenseTracker.Core.Dto.TransactionCategory;
using ExpenseTracker.Core.Exceptions;
using ExpenseTracker.Core.Logging;
using ExpenseTracker.Core.Repositories.Interface;
using ExpenseTracker.Core.Services.Interface;
using ExpenseTracker.Web.ViewModels.TransactionCategory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Web.Controllers
{
    [Authorize]
    public class TransactionCategoryController : Controller
    {
        private readonly ITransactionCategoryRepository _transactionCategoryRepository;
        private readonly ITransactionCategoryService _transactionCategoryService;
        private readonly IApplicationLogger<TransactionCategoryController> _logger;
        private readonly INotyfService _notifyService;

        public TransactionCategoryController(ITransactionCategoryRepository transactionCategoryRepository,
            ITransactionCategoryService transactionCategoryService,
            IApplicationLogger<TransactionCategoryController> logger,INotyfService notifyService)
        {
            _transactionCategoryRepository = transactionCategoryRepository;
            _transactionCategoryService = transactionCategoryService;
            _logger = logger;
            _notifyService = notifyService;
        }
        
        public async Task<IActionResult> Index(TransactionCategoryIndexViewModel transactionCategoryIndexViewModel)
        {
            var transactionCategories = await _transactionCategoryRepository.GetAllAsync();
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
            
                
                _notifyService.Success("Transaction Category Create Successfully");
                
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message,e);
                _notifyService.Error(e.Message);
            }
            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> Edit(int transactionCategoryId)
        {
            try
            {
                var transactionCategory = await _transactionCategoryRepository.FindAsync(transactionCategoryId)
                     ?? throw new TransactionCategoryNotFoundException();

                var transactionViewModel = new TransactionCategoryViewModel()
                {
                    TransactionCategoryId = transactionCategory.Id,
                    Name = transactionCategory.CategoryName,
                    Icon = transactionCategory.Icon,
                    Type = transactionCategory.Type,
                    Color = transactionCategory.Color
                };
            
                return View(transactionViewModel);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message,e);
                _notifyService.Error(e.Message);
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
            
                _notifyService.Success("Transaction Category Updated Successfully");
                
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message,e);
                _notifyService.Error(e.Message);
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int transactionCategoryId)
        {
            try
            {
                var transactionCategory = await _transactionCategoryRepository.FindAsync(transactionCategoryId)
                     ?? throw new TransactionCategoryNotFoundException();

                await _transactionCategoryService.Delete(transactionCategory.Id);
                
                _notifyService.Success("Transaction Category Deleted Successfully");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message,e);
                _notifyService.Error(e.Message);
            }
            return RedirectToAction(nameof(Index));
        }
        
        
        
    }
}