using System.Collections.Generic;
using ExpenseTracker.Contracts.Dto.Response;

namespace ExpenseTracker.WebApi.Response
{
    public class TransactionCategoryResponse : BaseResponse
    {
        public List<TransactionCategoryResponseDto> Categories { get; set; } = new();
    }
}

