﻿using System.Text.Json;
using ExpenseTracker.Contracts.Dto.Response;

namespace ExpenseTracker.WebApi.Response
{
    public class ErrorResponse : BaseResponse
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }
        public bool Success { get; set; }
        public override string ToString() => JsonSerializer.Serialize(this);
    }

}