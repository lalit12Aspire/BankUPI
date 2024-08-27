using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UPIWork.Models.Data;
using UPIWork.Models.Entity;
using UPIWork.Repository.Service;

namespace UPIWork.Repository.Helper
{
    public class TransactionRepository : Repository<Transaction>, ITransactionRepository
    {
        public TransactionRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<decimal> GetBalanceByUPIOrAccountNumberAsync(string upiId = null, string accountNumber = null)
        {
            var account = await _dbContext.Accounts
                .Where(a => (upiId != null && a.UPIID == upiId) || (accountNumber != null && a.AccountNumber == accountNumber))
                .Select(a => a.Balance)
                .FirstOrDefaultAsync();

            if (account == default)
            {
                throw new ArgumentException("Account not found for the provided UPI ID or Account Number.");
            }

            return account;
        }

        public async Task<IEnumerable<Transaction>> GetMiniStatementByUPIOrAccountNumberAsync(string upiId = null, string accountNumber = null, int numberOfTransactions = 5)
        {
            var accountId = await _dbContext.Accounts
                .Where(a => (upiId != null && a.UPIID == upiId) || (accountNumber != null && a.AccountNumber == accountNumber))
                .Select(a => a.AccountID)
                .FirstOrDefaultAsync();

            if (accountId == Guid.Empty)
            {
                throw new ArgumentException("Account not found for the provided UPI ID or Account Number.");
            }

            return await _dbContext.Transactions
                .Where(t => t.AccountID == accountId)
                .OrderByDescending(t => t.TransactionDate)
                .Take(numberOfTransactions)
                .ToListAsync();
        }

        public async Task<int> GetTransactionCountByUPIOrAccountNumberAsync(string upiId = null, string accountNumber = null)
        {
            var accountId = await _dbContext.Accounts
                .Where(a => (upiId != null && a.UPIID == upiId) || (accountNumber != null && a.AccountNumber == accountNumber))
                .Select(a => a.AccountID)
                .FirstOrDefaultAsync();

            if (accountId == Guid.Empty)
            {
                throw new ArgumentException("Account not found for the provided UPI ID or Account Number.");
            }

            return await _dbContext.Transactions
                .CountAsync(t => t.AccountID == accountId);
        }
    }
}
