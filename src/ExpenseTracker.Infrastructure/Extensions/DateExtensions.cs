using System;

namespace ExpenseTracker.Infrastructure.Extensions
{
    public static class DateExtensions
    {
        public static (DateTime, DateTime) GetDateBound(this DateTime date)
        {
            var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
            return (firstDayOfMonth, lastDayOfMonth);
        }
    }
}