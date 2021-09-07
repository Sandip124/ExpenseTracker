using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Common.Helpers.Alert
{
    
    public class Alert
    {
        public string Message { get; set; }
        public MessageType MessageType { get; set; }

    }
    public enum MessageType
    {
        Success,
        Error,
        Info
    }
    
   public class AlertHelper
    {
        public static void SetSuccessMessage(Controller controller, string message)
        {
            Alert alert = new() {Message = message, MessageType = MessageType.Success};
        }
        
        public static void SetErrorMessage(Controller controller, string message)
        {
            Alert alert = new() {Message = message, MessageType = MessageType.Error};
        }
        public static void SetInfoMessage(Controller controller, string message)
        {
            Alert alert = new() {Message = message, MessageType = MessageType.Info};
        }
    }
   
   
}
