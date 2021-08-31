namespace ExpenseTracker.Core.Helper.Alert
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
}
