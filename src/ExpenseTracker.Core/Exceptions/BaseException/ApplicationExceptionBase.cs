using System;

namespace ExpenseTracker.Core.Exceptions.BaseException;

public abstract class ApplicationExceptionBase: Exception
{
    protected ApplicationExceptionBase(string message) : base(message)
    {
    }
}