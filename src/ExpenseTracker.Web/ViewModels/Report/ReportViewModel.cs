using System;
using System.Collections.Generic;

namespace ExpenseTracker.Web.ViewModels.Report
{
    public class ReportViewModel
    {

        public DateTime? TransactionDate { get; set; }

        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    
        public IList<Core.Entities.Transaction> DailyTransactions { get; set; } = new List<Core.Entities.Transaction>();
        public IList<Core.Entities.Transaction> WeeklyTransactions { get; set; } = new List<Core.Entities.Transaction>();
        public IList<Core.Entities.Transaction> MonthlyTransactions { get; set; } = new List<Core.Entities.Transaction>();
        
        
    }
}