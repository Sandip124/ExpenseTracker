using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Core.Helper
{
    public class Alert
    {
        public string message { get; set; }
        public messageType message_type { get; set; }

    }
    public enum messageType
    {
        success,
        error
    }
}
