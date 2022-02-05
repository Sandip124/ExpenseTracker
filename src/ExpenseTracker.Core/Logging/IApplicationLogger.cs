namespace ExpenseTracker.Core.Logging;

public interface IApplicationLogger<T>
{
    void LogInformation(string message, params object[] args);
    void LogWarning(string message, params object[] args);
    void LogError(string message, params object[] args);
    void LogTrace(string message, params object[] args);
}