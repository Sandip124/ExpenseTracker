using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExpenseTracker.Core.Entities;
using ExpenseTracker.Core.Entities.Common;
using ExpenseTracker.Core.Repositories.Interface;
using ExpenseTracker.Infrastructure.Extensions;
using ExpenseTracker.Web.Providers.Interface;
using ExpenseTracker.Web.ViewModels.Report;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Web.Controllers
{
    [Authorize]
    public class ReportController : Controller
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IUserProvider _userProvider;

        public ReportController(ITransactionRepository transactionRepository,IUserProvider userProvider)
        {
            _transactionRepository = transactionRepository;
            _userProvider = userProvider;
        }
        // GET
        public async Task<IActionResult> Index(DailyReportViewModel reportViewModel)
        {
            var workspaceToken = (await _userProvider.GetCurrentUser()).DefaultWorkspace.Token;
            var transactionQueryable =
                _transactionRepository.GetPredicatedQueryable(a => a.Workspace.Token == workspaceToken);

            if (reportViewModel.TransactionDate != null)
            {
                transactionQueryable  = transactionQueryable.Where(a => a.TransactionDate.Date == reportViewModel.TransactionDate.Value.Date);
            }
            else
            {
                transactionQueryable  = transactionQueryable.Where(a => a.TransactionDate.Date == DateTime.Today.Date);
                reportViewModel.TransactionDate = DateTime.Now;
            }

            reportViewModel.DailyTransactions = transactionQueryable.Include(a=>a.TransactionCategory).OrderByDescending(a => a.EntryDate).ToList();
        
            return View(reportViewModel);
        }

        public async Task<IActionResult> Monthly(MonthlyReportViewModel reportViewModel)
        {
            var workspaceToken = (await _userProvider.GetCurrentUser()).DefaultWorkspace.Token;
            var transactionQueryable =
                _transactionRepository.GetPredicatedQueryable(a => a.Workspace.Token == workspaceToken);
                
            if (reportViewModel is { FromDate: { }, ToDate: { } })
            {
                transactionQueryable  = transactionQueryable.Where(a => a.TransactionDate.Date >= reportViewModel.FromDate.Value.Date && a.TransactionDate.Date <= reportViewModel.ToDate.Value.Date);
            }
            else
            {
                var (firstDay, lastDay) = DateTime.Now.GetDateBound();
                transactionQueryable  = transactionQueryable.Where(a => a.TransactionDate.Date >= DateTime.Today.AddMonths(-1).Date && a.TransactionDate.Date <= DateTime.Today.Date);
                reportViewModel.FromDate = firstDay;
                reportViewModel.ToDate = lastDay;
            }
            
            reportViewModel.MonthlyTransactions = transactionQueryable.Include(a=>a.TransactionCategory).OrderByDescending(a => a.EntryDate).ToList();
            
            return View(reportViewModel);
        }
        
        public async Task<IActionResult> StatementReport(StatementReportViewModel reportViewModel)
        {
            var workspaceToken = (await _userProvider.GetCurrentUser()).DefaultWorkspace.Token;
            var transactionQueryable =
                _transactionRepository.GetPredicatedQueryable(a => a.Workspace.Token == workspaceToken);
                
            if (reportViewModel is { FromDate: { }, ToDate: { } })
            {
                transactionQueryable  = transactionQueryable.Where(a => a.TransactionDate.Date >= reportViewModel.FromDate.Value.Date && a.TransactionDate.Date <= reportViewModel.ToDate.Value.Date);
            }
            else
            {
                var (firstDay, lastDay) = DateTime.Now.GetDateBound();
                transactionQueryable  = transactionQueryable.Where(a => a.TransactionDate.Date >= DateTime.Today.AddMonths(-1).Date && a.TransactionDate.Date <= DateTime.Today.Date);
                reportViewModel.FromDate = firstDay;
                reportViewModel.ToDate = lastDay;
            }

            var report = transactionQueryable.Include(a => a.TransactionCategory).OrderByDescending(a => a.EntryDate);
            reportViewModel.StatementReport = new ValueTuple<IList<Transaction>, IList<Transaction>>(
                report.Where(a => a.Type == TransactionType.Expense).ToList(),
                report.Where(a => a.Type == TransactionType.Income).ToList());
            
            return View(reportViewModel);
        }
    }
}