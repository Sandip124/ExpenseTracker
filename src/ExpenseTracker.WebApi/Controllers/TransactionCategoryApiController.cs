using ExpenseTracker.Contracts.Dto.Response;
using ExpenseTracker.Core.Entities.Common;
using ExpenseTracker.Core.Repositories.Interface;
using ExpenseTracker.WebApi.Response;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.WebApi.Controllers
{
    [ApiController]
    [Route("/api/transaction-categories/")]
    public class TransactionCategoryApiController : ApiControllerBase
    {
        private readonly ITransactionCategoryRepository _transactionCategoryRepository;

        public TransactionCategoryApiController(ITransactionCategoryRepository transactionCategoryRepository)
        {
            _transactionCategoryRepository = transactionCategoryRepository;
        }

        [HttpGet("{type}/type")]
        public async Task<IActionResult> GetCategoriesByType(string type)
        {
            if (!TransactionType.IsValidType(type))
            {
                return BadRequest("Transaction type not valid");
            }

            var transactionCategories = await _transactionCategoryRepository.GetByType(type);

            var transactionCategoryResponse = new TransactionCategoryResponse()
            {
                Categories = transactionCategories.Select(a => new TransactionCategoryResponseDto()
                {
                    Id = a.Id,
                    CategoryName = a.CategoryName,
                    Type = a.Type,
                    Icon = a.Icon,
                    Color = a.Color
                }).ToList()
            };
            
            return Ok(transactionCategoryResponse);
        }
    }
}