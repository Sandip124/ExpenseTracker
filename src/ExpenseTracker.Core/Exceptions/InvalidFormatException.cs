using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Core.Exceptions
{
     public class InvalidFormatException : ApplicationException
    {
        public InvalidFormatException(string message = "Invalid File Format") : base(message)
        {

        }
    }
}
