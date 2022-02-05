using ExpenseTracker.Core.Exceptions.BaseException;

namespace ExpenseTracker.Core.Exceptions
{
     public class InvalidFormatException : ApplicationExceptionBase
    {
        public InvalidFormatException(string message = "Invalid File Format") : base(message)
        {

        }
    }
}
