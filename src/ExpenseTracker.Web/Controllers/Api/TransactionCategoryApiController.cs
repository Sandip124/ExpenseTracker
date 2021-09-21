using System.Threading.Tasks;
using ExpenseTracker.Core.Entities.Common;
using ExpenseTracker.Core.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Web.Controllers.Api
{
    [ApiController]
    [Route("/api/TransactionCategory/")]
    public class TransactionCategoryApiController : ControllerBase
    {
        private readonly ITransactionCategoryRepository _transactionCategoryRepository;

        public TransactionCategoryApiController(ITransactionCategoryRepository transactionCategoryRepository)
        {
            _transactionCategoryRepository = transactionCategoryRepository;
        }

        [HttpGet("{type}/getByType")]
        public async Task<IActionResult> GetCategoriesByType(string type)
        {
            if (!TransactionType.IsValidType(type))
            {
                return BadRequest("Transaction type not valid");
            }

            var transactionCategories = await _transactionCategoryRepository.GetByType(type);

            return Ok(transactionCategories);
        }
    }
}