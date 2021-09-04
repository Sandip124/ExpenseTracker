using System.Threading.Tasks;
using ExpenseTracker.Core.Repositories.Interface;
using ExpenseTracker.Web.ViewModels.TransactionCategory;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Web.Controllers
{
    public class TransactionCategoryController : Controller
    {
        private readonly ITransactionCategoryRepository _transactionCategoryRepository;

        public TransactionCategoryController(ITransactionCategoryRepository transactionCategoryRepository)
        {
            _transactionCategoryRepository = transactionCategoryRepository;
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
    }
}