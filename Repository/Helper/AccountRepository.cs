using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UPIWork.Models.Data;
using UPIWork.Models.DTO;
using UPIWork.Models.Entity;
using UPIWork.Repository.Service;
using Microsoft.EntityFrameworkCore;

namespace UPIWork.Repository.Helper
{
    public class AccountRepository : Repository<Account>, IAccountRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public AccountRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        // Get Account by ID - return as DTO without exposing AccountID
        public async Task<AccountDTO> GetAccountByIdAsync(Guid accountId)
        {
            return await _dbContext.Accounts
                .Where(a => a.AccountID == accountId)
                .Select(a => new AccountDTO
                {
                    AccountNumber = a.AccountNumber,
                    Balance = a.Balance,
                    CardNumber = a.CardNumber,
                    CustomerId = a.CustomerId,
                    // Exclude AccountID
                })
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<AccountDTO>> GetAllAccountsAsync()
        {
            return await _dbContext.Accounts
                .Select(a => new AccountDTO
                {
                    AccountNumber = a.AccountNumber,
                    Balance = a.Balance,
                    CardNumber = a.CardNumber,
                    CustomerId = a.CustomerId,
                    // Exclude AccountID
                })
                .ToListAsync();
        }

        public async Task UpdateAccountAsync(Guid accountId, AccountDTO accountDTO)
        {
            var account = await _dbContext.Accounts.FindAsync(accountId);
            if (account != null)
            {
                account.AccountNumber = accountDTO.AccountNumber;
                account.Balance = accountDTO.Balance;
                account.CardNumber = accountDTO.CardNumber;
                // Update other properties as needed

                _dbContext.Accounts.Update(account);
                await _dbContext.SaveChangesAsync();
            }
        }

        // Delete Account - use AccountID internally
        public async Task DeleteAccountAsync(Guid accountId)
        {
            var account = await _dbContext.Accounts.FindAsync(accountId);
            if (account != null)
            {
                _dbContext.Accounts.Remove(account);
                await _dbContext.SaveChangesAsync();
            }
        }

        // Deposit Into Account - Use AccountID internally
        public async Task<bool> DepositIntoAccountAsync(Guid accountId, decimal amount)
        {
            var account = await _dbContext.Accounts.FindAsync(accountId);
            if (account != null)
            {
                account.Balance += amount;
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        // Withdraw From Account - Use AccountID internally
        public async Task<bool> WithdrawFromAccountAsync(Guid accountId, decimal amount)
        {
            var account = await _dbContext.Accounts.FindAsync(accountId);
            if (account == null || account.Balance < amount)
            {
                return false;
            }

            account.Balance -= amount;
            var transaction = new Transaction
            {
                TransactionID = Guid.NewGuid(),
                AccountID = accountId,
                TransactionDate = DateTime.Now,
               //TransactionType = "Withdrawal",
                AmountToBeDeposited = -amount
            };

            _dbContext.Transactions.Add(transaction);
            await _dbContext.SaveChangesAsync();
            return true;
        }

    }
}
