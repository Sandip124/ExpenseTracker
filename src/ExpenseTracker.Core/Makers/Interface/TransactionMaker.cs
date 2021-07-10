using ExpenseTracker.Core.Dto.Transaction;
using ExpenseTracker.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Core.Makers.Interface
{
   public interface TransactionMaker
    {
        void copy(ref Transaction transaction, TransactionCreateDto transactionCreateDto);
    }
}
