using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Core.Helper
{
   public class AlertHelper
    {
        public static void SetSuccessMessage(Controller controller, string message)
        {
            Alert alert = new Alert {Message = message, MessageType = MessageType.Success};
        }
        
        public static void SetErrorMessage(Controller controller, string message)
        {
            Alert alert = new Alert {Message = message, MessageType = MessageType.Error};
        }
        public static void SetInfoMessage(Controller controller, string message)
        {
            Alert alert = new Alert {Message = message, MessageType = MessageType.Info};
        }
    }
}
