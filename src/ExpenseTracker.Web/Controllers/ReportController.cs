using System;
using System.Linq;
using System.Threading.Tasks;
using ExpenseTracker.Core.Repositories.Interface;
using ExpenseTracker.Infrastructure.Extensions;
using ExpenseTracker.Web.Providers;
using ExpenseTracker.Web.Providers.Interface;
using ExpenseTracker.Web.ViewModels.Report;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> Index(ReportViewModel reportViewModel)
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

            reportViewModel.DailyTransactions = transactionQueryable.OrderByDescending(a => a.EntryDate).ToList();
        
            return View(reportViewModel);
        }

        public async Task<IActionResult> Monthly(ReportViewModel reportViewModel)
        {
            var workspaceToken = (await _userProvider.GetCurrentUser()).DefaultWorkspace.Token;
            var transactionQueryable =
                _transactionRepository.GetPredicatedQueryable(a => a.Workspace.Token == workspaceToken);
                
            if (reportViewModel.FromDate != null && reportViewModel.ToDate != null)
            {
                transactionQueryable  = transactionQueryable.Where(a => a.TransactionDate.Date >= reportViewModel.FromDate.Value.Date && a.TransactionDate.Date <= reportViewModel.ToDate.Value.Date);
            }
            else
            {
                transactionQueryable  = transactionQueryable.Where(a => a.TransactionDate.Date >= DateTime.Today.AddMonths(-1).Date && a.TransactionDate.Date <= DateTime.Today.Date);
                reportViewModel.FromDate = DateTime.Now.AddMonths(-1);
                reportViewModel.ToDate = DateTime.Now;
            }
            
            reportViewModel.MonthlyTransactions = transactionQueryable.OrderByDescending(a => a.EntryDate).ToList();
            
            return View(reportViewModel);
        }
    }
}