using System;
using System.Collections.Generic;

namespace ExpenseTracker.Web.ViewModels.Report
{
    public class ReportViewModel
    {
        public DateTime? TransactionDate { get; set; }

        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
    
    public class DailyReportViewModel : ReportViewModel
    {
        public IList<Core.Entities.Transaction> DailyTransactions { get; set; } = new List<Core.Entities.Transaction>();
    }
    
    public class WeeklyReportViewModel : ReportViewModel
    {
        public IList<Core.Entities.Transaction> WeeklyTransactions { get; set; } = new List<Core.Entities.Transaction>();

    }

    public class MonthlyReportViewModel : ReportViewModel
    {
        public IList<Core.Entities.Transaction> MonthlyTransactions { get; set; } = new List<Core.Entities.Transaction>();
    }

    public class StatementReportViewModel : ReportViewModel
    {
        public (IList<Core.Entities.Transaction>, IList<Core.Entities.Transaction>) StatementReport { get; set; }
    }
}