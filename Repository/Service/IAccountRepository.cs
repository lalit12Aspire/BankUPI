using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UPIWork.Models.Entity;
using UPIWork.Models.DTO;

namespace UPIWork.Repository.Service
{
    public interface IAccountRepository : IRepository<Account>
    {
        Task<AccountDTO> GetAccountByIdAsync(Guid AccountId);
        Task<IEnumerable<AccountDTO>> GetAllAccountsAsync();
        //   Task AddAccountAsync(AccountDTO accountDTO);
        Task DeleteAccountAsync(Guid accountId);
        Task UpdateAccountAsync(Guid accountId, AccountDTO accountDTO);

        Task<bool> DepositIntoAccountAsync(Guid accountId, decimal amount);
        Task<bool> WithdrawFromAccountAsync(Guid accountId, decimal amount);
    }
}
