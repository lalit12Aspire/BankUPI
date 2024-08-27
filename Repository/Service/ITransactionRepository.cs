using System.Collections.Generic;
using System.Threading.Tasks;
using UPIWork.Models.Entity;

namespace UPIWork.Repository.Service
{
    public interface ITransactionRepository : IRepository<Transaction>
    {
        Task<int> GetTransactionCountByUPIOrAccountNumberAsync(string upiId = null, string accountNumber = null); // Get the transaction count for the day
        Task<IEnumerable<Transaction>> GetMiniStatementByUPIOrAccountNumberAsync(string upiId = null, string accountNumber = null, int numberOfTransactions = 5); // Generate a mini statement
        Task<decimal> GetBalanceByUPIOrAccountNumberAsync(string upiId = null, string accountNumber = null); // View balance
    }
}
