using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Core.Extensions
{
    public static class Notify
    {
        private const string Success = "success-message";
        private const string Error = "error-message";
        private const string Info = "info-message";

        public static void AddSuccessMessage(this Controller controller, string message) => SetMessage(controller, message, Success);

        public static void AddErrorMessage(this Controller controller, string message) => SetMessage(controller, message, Error);

        public static void AddInfoMessage(this Controller controller, string message) => SetMessage(controller, message, Info);

        private static void SetMessage(Controller controller, string message,string type)
        {
            var messageList = GetMessageListByIndex(controller, type);
            messageList.Add(message);
            SetMessageListByIndex(controller, messageList, type);
        }

        private static List<string> GetMessageListByIndex(Controller c, string type)
        {
            return c.TempData[type] as List<string> ?? new List<string>();
        }

        private static void SetMessageListByIndex(Controller controller, List<string> messageList, string type)
        {
            controller.TempData[type] = messageList;
        }
    }
}